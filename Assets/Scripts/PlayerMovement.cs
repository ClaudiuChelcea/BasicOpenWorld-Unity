using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Variables
	public float player_speed = 0f;
	public Rigidbody player;
	public Transform cameraTransform;
	public float rotationSpeed = 0f;
	public float jumpForce = 0f;
	public CapsuleCollider myCapsule;
	public float myTreshhold = 0f;
	private bool is_on_the_ground = true;
	private Animator animator;
	private Vector3 moveDirection;

	// Start is called before the first frame update
	void Start()
	{
		player = GetComponent<Rigidbody>();
		myCapsule = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();
		player.rotation = cameraTransform.rotation * Quaternion.AngleAxis(270f, new Vector3(0, 1, 0));
	}

	// Update is called once per frame
	void Update()
	{
		// Move the player by animation
		MovePlayer();

		// Player`s rotation
		PlayerRotation();

		// Sprint if left shift is being held
		Sprint();

		// Jump & see if the player touches the ground ( activate gizmos to see the lines )
		Jump();

		// Play attack animation
		CheckAttack();
	}

	// Player`s rotation by following camera
	private void PlayerRotation()
	{
		myCapsule.transform.rotation = Quaternion.Slerp(myCapsule.transform.rotation, cameraTransform.rotation, 5f);
		//myCapsule.transform.rotation = Quaternion.Slerp(myCapsule.transform.rotation, new Quaternion(cameraTransform.rotation.x, cameraTransform.rotation.y, cameraTransform.rotation.z, cameraTransform.rotation.w), 5f);
	}

	// If the player clicks left mouse button, attack
	private void CheckAttack()
	{
		// Can attack only on the ground
		if (is_on_the_ground == true)
		{
			// Check if the player attacks
			if (Input.GetButtonDown("Fire1"))
			{
				// Slow player when hitting
				animator.SetTrigger("Punch");
			}
		}
	}

	// Increase speed while holding left shift
	private void Sprint()
	{
		// Increase speed while holding left shift
		if (Input.GetButton("Fire3"))
		{
			player_speed = 10;
		}
		else
		{
			player_speed = 6;
		}
	}

	// Move the player based on user`s input
	private void MovePlayer()
	{
		// Get input
		float x_Axis = Input.GetAxis("Horizontal"); // A and D
		float z_Axis = Input.GetAxis("Vertical"); // W and S
		moveDirection = (cameraTransform.forward * z_Axis + cameraTransform.right * x_Axis).normalized * player_speed;

		// Move player
		Vector3 charSpace = transform.InverseTransformDirection(moveDirection);
		animator.SetFloat("Forward", charSpace.x, 0.075f, Time.deltaTime);
		animator.SetFloat("Right", charSpace.z, 0.075f, Time.deltaTime);
	}

	// Change velocity with animator
	private void OnAnimatorMove()
	{
		float get_y = player.velocity.y;
		player.velocity = moveDirection;
		player.velocity = new Vector3(moveDirection.x, get_y, moveDirection.z);
	}

	// Jump & check if we are touching the ground
	private void Jump()
	{
		// Check if we can jump by raycasting to the ground lines in the shape of our capsule
		int count_jump = CountIfGrounded();

		// If at least one part of the character touches the ground, you can jump
		AddJumpForceIfNeeded(count_jump);

		// For the jump animation
		JumpAnim();
	}

	// Set the variable is_on_the_ground & add force based on the number of jumps
	private void AddJumpForceIfNeeded(int count_jump)
	{
		if (count_jump >= 1)
		{
			is_on_the_ground = true;
			if (Input.GetButtonUp("Jump"))
			{
				player.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			}
		}
		else
		{
			// Otherwise, you can`t jump
			is_on_the_ground = false;
		}
	}

	// Play the jump animation
	private void JumpAnim()
	{
		if (is_on_the_ground)
		{
			animator.SetBool("Grounded", true);
		}
		else
		{
			animator.SetBool("Grounded", false);
		}
	}

	// Count in how many spots the player is touching the ground
	private int CountIfGrounded()
	{
		int count_jump = 0;
		for (float xOffset = -1; xOffset <= 1; ++xOffset)
		{
			for (float zOffset = -1; zOffset <= 1; ++zOffset)
			{
				if (Physics.Raycast(new Ray(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, Vector3.down / 6f), myTreshhold))
				{
					Debug.DrawRay(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, (Vector3.down * myTreshhold) / 2f, Color.green);
					count_jump++;
				}
				else
				{
					Debug.DrawRay(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, (Vector3.down * myTreshhold) / 2f, Color.red);
				}
			}
		}
		return count_jump;
	}
}