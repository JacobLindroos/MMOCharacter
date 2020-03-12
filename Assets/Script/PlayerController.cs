using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

    private float currentSpeed;
    private float speedSmoothVelocity;
    private float turnSmoothVelocity;
    private Transform cameraT;

    private void Start()
    {
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //keyboard input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            //calculating the target rotation of player
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

            //making the turning of the player more smoothly
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

        bool running = Input.GetKey(KeyCode.LeftShift);

        //if we are running, run speed is true otherwise walk speed. If theres no input the speed is zero, standing still
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

        //making the speed of player more smoothly
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //setting the move direction
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
    }
}
