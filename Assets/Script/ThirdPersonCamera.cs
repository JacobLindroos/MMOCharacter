using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	[Header("Mouse settings")]
	public Transform playerCenterPoint;
    public float mouseSensitivity = 10f;
	public float rotationSmoothTime = .12f;
	public Vector2 pitchMinMax = new Vector2(-40f, 85f);
	public bool hideCursor;

	[Header("Zoom settings")]
	public float zoomSpeed = 2f;
	public float zoomMin = 2f;
	public float zoomMax = 10f;
    
	private float zoomOffset = 2f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float yaw;
    private float pitch;

    private void Start()
    {
		CursorHandler();
    }

    // Update is called once per frame
    void LateUpdate()
    {
		ZoomHandler();

		CameraMovement();
    }


	/// <summary>
	/// Handles the zoom in/out function
	/// </summary>
	/// <returns> the current zoom value </returns>
	float ZoomHandler()
	{
		//adds or subtracts to the zoom value when mouse scroll wheel is used, times a speed value
		zoomOffset += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		if (zoomOffset < zoomMin)
		{
			zoomOffset = zoomMin;
		}
		if (zoomOffset > zoomMax)
		{
			zoomOffset = zoomMax;
		}

		return zoomOffset;
    }


	/// <summary>
	/// Handles the cameras movement
	/// </summary>
    private void CameraMovement()
    {
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
		transform.position = playerCenterPoint.position - transform.forward * zoomOffset;
	}


	/// <summary>
	/// Hides or shows the cursor in game
	/// </summary>
	private void CursorHandler()
	{
		//Hides the cursor in play mode
		if (hideCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
