using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentScript : Fighter
{
	private NavMeshAgent agent;
	public Transform player_transform;
	public bool is_aggressive = true;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		GetComponents();
		animator.ResetTrigger("Punch");
		StartCoroutine(AggressivenessCoroutine(0.8f));
	}
	
	// Set opponent`s aggressiveness
	IEnumerator AggressivenessCoroutine(float time)
	{
		yield return new WaitForSeconds(2f);
		is_aggressive = Random.Range(0,3)  !=  0; // Aggressive 3/4 of the time
		yield return StartCoroutine(AggressivenessCoroutine(1f));
	}

	// Update is called once per frame
	void Update()
	{
		// Attack the player
		agent.SetDestination(player_transform.position);

		// Move player
		SetAnimatorMovement();

		// Slow the enemy when attacking
		SlowPlayerWhenMakingAction();

		// Falling animation
		Jump();

		// Attack the player if the distance is little
		AttackPlayerWhenClose();

		// Get the moving direction
		moveDirection = agent.velocity.normalized;

		// Get animator state
		animatorState = animator.GetCurrentAnimatorStateInfo(0);
	}

	// Attach the player when the distance is little
	private void AttackPlayerWhenClose()
	{
		// Prevent attacking if we are already being attacked
		if (animatorState.IsName("TakeHit"))
			return;

		// Prevent attacking if the distance is too big
		if (Vector3.Distance(agent.nextPosition, player_transform.position) > 3f)
			return;

		else if (agent.remainingDistance < 1.2f && is_aggressive)
		{
			animator.SetTrigger("Punch");
		}
	}

	// Charge to the player
	void SetAnimatorMovement()
	{
		Vector3 charDirection = transform.InverseTransformDirection(moveDirection);
		animator.SetFloat("Forward", charDirection.x, 0.075f, Time.deltaTime);
		animator.SetFloat("Right", charDirection.z, 0.075f, Time.deltaTime);
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
	public  override void Jump()
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
