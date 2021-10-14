using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Fighter
{
	// Variablees
	public float rotationSpeed = 0f;
	private bool want_to_ressurect = false;
	private bool game_over;
	
	// Start is called before the first frame update
	void Start()
	{
		// Time.timeScale = 0.1f; // - Debug
		GetComponents();
		player.rotation = cameraTransform.rotation * Quaternion.AngleAxis(270f, new Vector3(0, 1, 0));
		animator = GetComponent<Animator>();
		health = 200;
	}

	// Update is called once per frame
	void Update()
	{
		// Check game status
		if (health <= 0)
		{
			if(game_over == false)
			{
				get_current_speed = player_speed;
			}
			game_over = true;
			player_speed = 0;
		}

		// Stop character if it`s dead
		StopCharacter();

		// Check if dead
		if (DeadAnim() == 1)
			return;

		// Update animator`s state
		animatorState = animator.GetCurrentAnimatorStateInfo(0);

		// Slow the player`s speed if he is attacking
		SlowPlayerWhenMakingAction();

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

		// Update speed
		get_current_speed = player_speed;

		// Taunt
		TauntAnimation();

		// Dance
		DanceAnimation();
	}

	// Stop character`s movement
	private void StopCharacter()
	{
		if (animatorState.IsName("DeadAnimation") || game_over == true)
		{
			get_current_speed = player_speed;
			player_speed = 0;
		}
		else
		{
			player_speed = get_current_speed;
		}
	}

	// Revive player if dead and presses R
	private void RevivePlayerIfWanted()
	{
		if (Input.GetKeyDown(KeyCode.R) && end_game == true)
		{
			want_to_ressurect = true;
			health = 200;
		}

		if (want_to_ressurect == true)
			Revive();
	}

	// Play dance animation
	private void DanceAnimation()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			animator.SetTrigger("Dance");
		}
		if (Input.GetKeyUp(KeyCode.Y))
		{
			animator.ResetTrigger("Dance");
		}
	}

	// Play taunt animation
	private void TauntAnimation()
	{
		if (Input.GetKeyUp(KeyCode.T))
		{
			animator.SetTrigger("Taunt");
		}
	}

	// Player`s rotation by following camera
	private void PlayerRotation()
	{
		if(animatorState.IsName("Punch"))
			return;
		myCapsule.transform.rotation = Quaternion.Slerp(myCapsule.transform.rotation, cameraTransform.rotation, 5f);
	}

	// If the player clicks left mouse button, attack
	private void CheckAttack()
	{
		// Prevent attacking if i am hit
		if (animatorState.IsName("TakeHit"))
			return;

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

	// Check if health
	public override int DeadAnim()
	{
		// Ressurect player if it is dead and user presses R key
		RevivePlayerIfWanted();

		if (health > 0)
			end_game = false;

		if (end_game == true)
			return 1;

		if (health <= 0)
		{
			end_game = true;
			animator.Play("DeadAnimation");
			want_to_ressurect = false;
		};

		if (end_game == true)
		{ 
			return 1;
			want_to_ressurect = false;
		}
		else
			return 0;
	}

	// Revive
	public virtual void Revive()
	{
		if (end_game == true)
		{
			end_game = false;
			animator.Play("Resurrection");
			player_speed = 6;
			get_current_speed = 6;
		}
	}
}