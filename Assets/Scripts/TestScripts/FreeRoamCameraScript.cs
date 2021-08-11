using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can be attached to a camera for free-roaming while playing with usage of recording in-game.
// Inputs:
/* 
 * Movement with WASD
 * Rotation with mouse
 * Up and down  via mouse rotation and WASD or with E & Q keys ( press / hold )
 * Sprint: LeftShift (h0ld)
 * Slow: CapsLock (hold) or F
 * ExtraSlow: Tab ( hold ) or G
 */

public class FreeRoamCameraScript : MonoBehaviour
{
        // Variables
        public float CameraRotationSpeed = 0f;
        public float CameraDefaultMovementSpeed = 0f;
	public float CameraGoUpSpeed = 0f;
	private float CameraUpStepDistance = 0;
	public float CameraRotationFluidity = 0f;
	public float GroundOffset = 0f;
	private float Move_X = 0f, Move_Z = 0f;
	private Vector3 moveDirection =new  Vector3(0,0,0);

	// For sprint
	private float get_current_speed_1 = 0f;
	private bool is_moving_slow = true;

	// For slow movement
	private float get_current_speed_2 = 0f;
	private bool is_moving_fast = true;

	// For extra slow movement
	private float get_current_speed_3 = 0f;
	private float extra_slow = 0.35f;
	private bool is_extra_slow = false;

	// For rotation
	private float yaw = 0;
	private float pitch = 0;

	// Start is called before the first frame update
	void Start()
        {
		// The position the camera starts
                transform.position = new Vector3(0, GroundOffset, 0);
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

		// Adjust camera`s height
		ChangeHeight();
	}

	// Add / subtract from camera`s y position
	private void ChangeHeight()
	{
		transform.position += Vector3.up * CameraUpStepDistance;
	}

	// Recalculate move direction
	private void CalculateMoveDirection()
	{
		moveDirection += (transform.forward * Move_Z + transform.right * Move_X).normalized * CameraDefaultMovementSpeed;
	}

	// Move the camera extra slow - for building interiors
	private void ExtraSlow()
	{
		if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.G))
		{
			if (is_extra_slow == false)
				get_current_speed_3 = CameraDefaultMovementSpeed;

			is_extra_slow = true;
			CameraDefaultMovementSpeed = extra_slow;
		}

		if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.G))
		{
			is_extra_slow = false;
			CameraDefaultMovementSpeed = get_current_speed_1;
		}
	}

	// Move camera slow - for walking
	private void MoveSlow()
	{
		if (Input.GetKeyDown(KeyCode.CapsLock) || Input.GetKeyDown(KeyCode.F))
		{
			if (is_moving_fast == true)
				get_current_speed_2 = CameraDefaultMovementSpeed;

			is_moving_fast = false;
			CameraDefaultMovementSpeed = get_current_speed_1 / (float)4;
		}

		if (Input.GetKeyUp(KeyCode.CapsLock) || Input.GetKeyUp(KeyCode.F))
		{
			is_moving_fast = false;
			CameraDefaultMovementSpeed = get_current_speed_1;
		}
	}

	// Move camera fast - for map traversal
	private void Sprint()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (is_moving_slow == true)
				get_current_speed_1 = CameraDefaultMovementSpeed;

			is_moving_slow = false;
			CameraDefaultMovementSpeed = get_current_speed_1 * 2;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			is_moving_slow = true;
			CameraDefaultMovementSpeed = get_current_speed_1;
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
