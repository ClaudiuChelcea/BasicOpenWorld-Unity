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
		myCapsule.transform.rotation = Quaternion.Slerp(myCapsule.transform.rotation, cameraTransform.rotation, 5f);

		// Sprint if left shift is being held
		Sprint();

		// Jump & see if the player touches the ground ( activate gizmos to see the lines )
		Jump();
	}

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
		// Draw line to the ground to see if we are on the ground or not
		if (is_on_the_ground == true) Debug.DrawRay(player.transform.position, Vector3.down / 5f, Color.green, 2);
		else Debug.DrawRay(player.transform.position, Vector3.down / 5f, Color.red, 2);

		// If he touches the ground, you can jump
		if (Physics.Raycast(new Ray(player.transform.position, Vector3.down), myTreshhold)) // send ray
		{
			is_on_the_ground = true;
			if (Input.GetButtonUp("Jump"))
			{
				player.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			}
		}
		else
		{ // Otherwise, you can`t jump
			is_on_the_ground = false;
		}

		// For the jump animation
		if (is_on_the_ground)
		{
			animator.SetBool("Grounded", true);
		}
		else
		{
			animator.SetBool("Grounded", false);
		}
	}
}