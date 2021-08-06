﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
        // Variables
        float yaw = 0f;
        float pitch = 0f;
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
                yaw += Input.GetAxis("Mouse X");
                pitch -= Input.GetAxis("Mouse Y");

                // Rotation
                if(pitch > -20 && pitch <=10)
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0f), rotationSpeed);
		else
		{
                        if(pitch <= -20)
                                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-20f, yaw, 0f), rotationSpeed);
                        else
                                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10f, yaw, 0f), rotationSpeed);
                }

                // Position
                transform.position = playerTransform.position + transform.TransformDirection(cameraOffset);
         }
}
