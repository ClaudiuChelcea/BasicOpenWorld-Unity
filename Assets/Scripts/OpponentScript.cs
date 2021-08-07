using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentScript : Fighter
{
	private NavMeshAgent agent;
	public Transform player_transform;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		GetComponents();
		animator.ResetTrigger("Punch");
	}

	// Update is called once per frame
	void Update()
	{
		agent.SetDestination(player_transform.position);

		// Move player
		SetAnimatorMovement();

		SlowPlayerWhenAttacking();

		Jump();

		if(agent.remainingDistance < 1f)
		{
			animator.SetTrigger("Punch");
		}
	}

	// Charge to the player
	private void SetAnimatorMovement()
	{
		animator.SetFloat("Forward", player_transform.position.x, 0.075f, Time.deltaTime);
		animator.SetFloat("Right", player_transform.position.z, 0.075f, Time.deltaTime);
	}

	// Count in how many spots the player is touching the ground
	public override int CountIfGrounded()
	{
		int count_jump = 0;
		for (float xOffset = -1; xOffset <= 1; ++xOffset)
		{
			for (float zOffset = -1; zOffset <= 1; ++zOffset)
			{
				if (Physics.Raycast(new Ray(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, Vector3.down / 6f), myTreshhold / 6f))
				{
					Debug.DrawRay(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, Vector3.down / 6f, Color.green);
					count_jump++;
				}
				else
				{
					Debug.DrawRay(myCapsule.transform.position + (myCapsule.transform.forward * xOffset + myCapsule.transform.right * zOffset).normalized, Vector3.down / 6f, Color.red);
				}
			}
		}
		return count_jump;
	}

	// Falling
	private  void Jump()
	{
		// Check if we can jump by raycasting to the ground lines in the shape of our capsule
		int count_jump = CountIfGrounded();

		if (count_jump <= 1)
			is_on_the_ground = false;
		else
			is_on_the_ground = true;

		JumpAnim();
	}
}
