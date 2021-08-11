using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can be attached to a camera for free-roaming while playing with usage of recording in-game.
// Inputs:
/* 
 * Movement with WASD
 * Rotation with mouse
 * Up and down  via mouse rotation and WASD or with E & Q keys ( press / hold )
 * Sprint: LeftShift (keypress)
 * Slow: CapsLock / F (keypress)
 * ExtraSlow: Tab / G (keypress)
 * Default movement speed: R (keypress)
 */

public class FreeRoamCameraScript : MonoBehaviour
{
	// Variables
	public float CameraRotationSpeed = 0f;
	public float CameraGoUpSpeed = 0f;
	private float CameraUpStepDistance = 0;
	public float CameraRotationFluidity = 0f;
	public float GroundOffset = 0f;
	private float Move_X = 0f, Move_Z = 0f;
	private Vector3 moveDirection = new Vector3(0, 0, 0);
	float default_Speed = 0f;
	public float extra_Slow_Speed = 0f;
	public float slow_Speed = 0f;
	public float base_Speed = 0f;
	public float fast_Speed = 0f;
	public float Display_Camera_Speed = 0f;

	// For rotation
	private float yaw = 0;
	private float pitch = 0;

	// Start is called before the first frame update
	void Start()
        {
		// The position the camera starts
                transform.position = new Vector3(0, GroundOffset, 0);
		default_Speed = base_Speed;
		Display_Camera_Speed = default_Speed;
        }

        // Update is called once per frame
        void Update()
	{
		// Camera rotation with mouse
		CameraRotation();

		// Go up and down
		UpAndDown();

		// Move
		ApplyPositionChange();

		// Recalculate move direction
		CalculateMoveDirection();

		// Sprint, move slow or move extra slow based on user`s input
		Sprint();
		MoveSlow();
		ExtraSlow();
		ResetSpeed();

		// Adjust camera`s height
		ChangeHeight();

		// Update displayed camera speed
		Display_Camera_Speed = default_Speed;
	}

	// Add / subtract from camera`s y position
	private void ChangeHeight()
	{
		transform.position += Vector3.up * CameraUpStepDistance;
	}

	// Recalculate move direction
	private void CalculateMoveDirection()
	{
		moveDirection += (transform.forward * Move_Z + transform.right * Move_X).normalized * default_Speed;
	}

	// Move the camera extra slow - for building interiors
	private void ExtraSlow()
	{
		if (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.G))
		{
			default_Speed = extra_Slow_Speed; 
		}
	}

	// Set the camera`s speed to the default speed
	private void ResetSpeed()
	{
		if(Input.GetKey(KeyCode.R))
		{
			default_Speed = base_Speed; ;
		}
	}

	// Move camera slow - for walking
	private void MoveSlow()
	{
		if (Input.GetKey(KeyCode.CapsLock) || Input.GetKey(KeyCode.F))
		{
			default_Speed = slow_Speed;
		}
	}

	// Move camera fast - for map traversal
	private void Sprint()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			default_Speed = fast_Speed;
		}
	}

	// Change camera`s position
	private void ApplyPositionChange()
	{
		WASD();
		transform.position = moveDirection;
	}

	// Get input
	private void WASD()
	{
		Move_X = Input.GetAxis("Horizontal");
		Move_Z = Input.GetAxis("Vertical");
	}

	// Go up or down on the y axis
	private void UpAndDown()
	{
		if (Input.GetKey(KeyCode.E))
		{
			CameraUpStepDistance += CameraGoUpSpeed;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			CameraUpStepDistance -= CameraGoUpSpeed;
		}
	}

	// Rotate camera by mouse rotation
	private void CameraRotation()
	{
		yaw += Input.GetAxis("Mouse X");
		pitch += Input.GetAxis("Mouse Y");
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-pitch * CameraRotationSpeed , yaw * CameraRotationSpeed,  transform.rotation.z), CameraRotationFluidity);
	}
}
