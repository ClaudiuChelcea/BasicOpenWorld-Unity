using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
	// Variables
	public float player_speed = 0f;
	public Rigidbody player;
	public CapsuleCollider myCapsule;
	public float myTreshhold = 0f;
	protected bool is_on_the_ground = true;
	protected Animator animator;
	protected Vector3 moveDirection;
	protected AnimatorStateInfo animatorState;
	protected float get_current_speed = 6f;
	public Transform cameraTransform;
	public float jumpForce = 0f;

	// Initialise components
	public void GetComponents()
	{
		player = GetComponent<Rigidbody>();
		myCapsule = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();
	}

	// Slow the player when the attack animation is playing
	public  void SlowPlayerWhenAttacking()
	{
		if (animatorState.IsName("RightPunch") || animatorState.IsName("LeftPunch"))
		{
			player_speed = 2;
		}
		else
		{
			player_speed = get_current_speed;
		}
	}

	// Play the jump animation
	public  void JumpAnim()
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
	public virtual int CountIfGrounded()
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

	// Set the variable is_on_the_ground & add force based on the number of jumps
	public virtual void AddJumpForceIfNeeded(int count_jump)
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

	// Jump & check if we are touching the ground
	public virtual void Jump()
	{
		// Check if we can jump by raycasting to the ground lines in the shape of our capsule
		int count_jump = CountIfGrounded();

		// If at least one part of the character touches the ground, you can jump
		AddJumpForceIfNeeded(count_jump);

		// For the jump animation
		JumpAnim();
	}

}
