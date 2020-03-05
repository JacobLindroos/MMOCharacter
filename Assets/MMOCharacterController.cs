using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOCharacterController : MonoBehaviour
{
    public Transform playerCamera, character, centerPoint;

    public float mouseSensitivity = 10f;
    public float mouseYPosition = 1f;
    public float moveSpeed = 2f;
    public float zoomSpeed = 2f;
    public float zoomMin = -2f;
    public float zoomMax = -10f;
    public float rotationSpeed = 5f;

	private float zoom;
	private float mouseX, mouseY;
    private float mouseYMin = -60f;
    private float mouseYMax = 60f;
    private float moveFB, moveLR; //move front back and left right
	private bool isTurning;

    // Start is called before the first frame update
    void Start()
    {
        zoom = -1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Handles the zoom function using mouse scroll wheel
        ZoomHandler();

        //Handles the rotation of the camera around the player, using the mouse X and Y
        CameraMouseOrbit();

		//Handles the players movement
		PlayerMovement();
    }


    void ZoomHandler()
    {
		//adds or subtracts to the zoom value when mouse scroll wheel is used, times a speed value
		zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		if (zoom > zoomMin)
		{
			zoom = zoomMin;
		}
		if (zoom < zoomMax)
		{
			zoom = zoomMax;
		}

		playerCamera.transform.localPosition = new Vector3(0, 0, zoom);
	}

    void CameraMouseOrbit()
    {
		//if holding down right mouse button the mouseX and Y is updated when moving the mouse around
		if (Input.GetMouseButton(1))
		{
			mouseX += Input.GetAxis("Mouse X");
			mouseY += Input.GetAxis("Mouse Y");
		}

		//sets a min and max value for mouseY angle, cap's the vertical movement of the camera
		mouseY = Mathf.Clamp(mouseY, mouseYMin, mouseYMax);
		//rotates the camera around the center point using the mouse X and Y
		centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

		//keeps the camera always looking at the center point
		playerCamera.LookAt(centerPoint);
	}

    void PlayerMovement()
    {
		//Updates the players front, back, left and right movement
		moveFB = Input.GetAxis("Vertical") * moveSpeed;
		moveLR = Input.GetAxis("Horizontal") * moveSpeed;

		Vector3 movement = new Vector3(moveLR, 0, moveFB);

        //change the rotation of the movement based on the camera
   		movement = character.rotation * movement;

		//moves the player using it's character controller 
		character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);

		//keeps the center point at the players position
		centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);


		if (Input.GetMouseButton(0))
		{
			isTurning = true;
		}

		if (isTurning)
		{
			//if players is moved forward and backwards
			//if (Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0)
			//{
				//calculates the turn angle for the player, using the center points new angle
				Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);

				//rotates the player towards the turn angle
				character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);

			Quaternion characterAngle = Quaternion.Euler(0, character.eulerAngles.y, 0);

			Debug.Log("charAngle: " + characterAngle);
			Debug.Log("turn: " + turnAngle);


			if (characterAngle == turnAngle)
					{
						isTurning = false;
					}
				//}
		}
	}
}
