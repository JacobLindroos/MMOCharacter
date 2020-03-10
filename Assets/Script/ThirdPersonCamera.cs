using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10f;
    public Transform target;
    public float offset = 2f;
    public Vector2 pitchMinMax = new Vector2(-40f, 85f);
    public float rotationSmoothTime = .12f;

	public float zoomSpeed = 2f;
	public float zoomMin = 2f;
	public float zoomMax = 10f;
    private float zoom = 2f;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float yaw;
    private float pitch;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {

		ZoomHandler();

		//updating the yaw and pitch depending upon the mouse input
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        //setting a min and max angle for vertical movement
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        //making the rotation more smoothly
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        //setting the rotation of the camera
        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;

        //setting the position of the camera
        transform.position = target.position - transform.forward * zoom;
    }

	float ZoomHandler()
	{
		//adds or subtracts to the zoom value when mouse scroll wheel is used, times a speed value
		zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		if (zoom < zoomMin)
		{
			zoom = zoomMin;
		}
		if (zoom > zoomMax)
		{
			zoom = zoomMax;
		}

		return zoom;
    }
}
