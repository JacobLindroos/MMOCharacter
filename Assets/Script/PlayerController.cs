using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

	private bool isRunning;
    private float currentSpeed;
    private float speedSmoothVelocity;
    private float turnSmoothVelocity;
    private Transform cameraT;

    private void Start()
    {
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //keyboard input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
			PlayerRotation(inputDir);
		}

        isRunning = Input.GetKey(KeyCode.LeftShift);

		PlayerMovement(isRunning, inputDir);
    }


	/// <summary>
	/// Sets rotation of player
	/// </summary>
	/// <param name="inputDir"> takes in the input direction from keyboard </param>
	private void PlayerRotation(Vector2 inputDir)
	{
		//calculating the target rotation of player
		float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

		//setting the rotation of player, making the turning of the player more smoothly
		transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
	}


	/// <summary>
	/// Sets player movement
	/// </summary>
	/// <param name="isRunning"> true or false </param>
	/// <param name="inputDir"> takes in the input direction from keyboard </param>
	private void PlayerMovement(bool isRunning, Vector2 inputDir)
	{
		//if we are running, run speed is true otherwise walk speed. If theres no input the speed is zero, standing still
		float targetSpeed = ((isRunning) ? runSpeed : walkSpeed) * inputDir.magnitude;

		//making the speed of player more smoothly
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		//setting the move direction
		transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
	}
}

