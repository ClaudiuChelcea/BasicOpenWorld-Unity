using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	// Variables
	private float yaw = 0f;
	private float pitch = 0f;
	public Transform playerTransform;
	public float heightToPlayer = 2f;
	public Vector3 cameraOffset;
	public float rotationSpeed = 0f;

	// Start is called before the first frame update
	void Start()
	{
		transform.rotation = playerTransform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		// Get user`s mouse input
		yaw += Input.GetAxis("Mouse X");
		pitch -= Input.GetAxis("Mouse Y");

		// Rotation
		SetCameraRotation();

		// Adjust position to follow the player in third person view
		SetCameraPosition();
	}

	private void SetCameraPosition()
	{
		transform.position = playerTransform.position + transform.TransformDirection(cameraOffset);
	}

	private void SetCameraRotation()
	{
		if (pitch > -20 && pitch <= 10) // Lock camera to certain view on the vertical axis
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0f), rotationSpeed);
		}
		else
		{  // Lock camera`s up and down view
			if (pitch <= -20) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-20f, yaw, 0f), rotationSpeed);
			else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10f, yaw, 0f), rotationSpeed);
		}
	}
}