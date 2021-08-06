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
	Animator animator;

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
		// Get input
		float x_Axis = Input.GetAxis("Horizontal");  // A and D
		float z_Axis = Input.GetAxis("Vertical");  // W and S
		Vector3 moveDirection = (cameraTransform.forward * z_Axis + cameraTransform.right * x_Axis).normalized * player_speed;

		// Move player
		var get_actual_velocity = player.velocity.y;
		// player.velocity = new Vector3(player.velocity.x, get_actual_velocity, player.velocity.z); // Update vertical movement - no longer use it because we use the animator
		Vector3 charSpace = transform.InverseTransformDirection(moveDirection);
		animator.SetFloat("Forward", charSpace.x);
		animator.SetFloat("Right", charSpace.z);

		// Fix rotation
		//if (moveDirection.magnitude > 10e-3f)
		//{
		//	player.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraTransform.forward * z_Axis + cameraTransform.right * x_Axis, Vector3.up), Time.deltaTime * rotationSpeed);
		//}

		player.rotation = cameraTransform.rotation;

		// Jump
		// See if the player touches the ground ( activate gizmos to see the lines )
		Jump();

	}

	private void Jump()
	{
		if (is_on_the_ground == true)
			Debug.DrawRay(player.transform.position, Vector3.down / 5f, Color.green, 2);
		else
			Debug.DrawRay(player.transform.position,  Vector3.down / 5f, Color.red, 2);

		// If he touches the ground
		if (Physics.Raycast(new Ray(player.transform.position, Vector3.down),  myTreshhold))
		{
			is_on_the_ground = true;
			if (Input.GetButtonUp("Jump"))
			{
				player.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			}
		}
		else
			is_on_the_ground = false;
	}
}
