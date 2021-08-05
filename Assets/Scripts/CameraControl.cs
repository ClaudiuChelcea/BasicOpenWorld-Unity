using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Variables
    float yaw = 0f;
    float pitch = 0f;
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;
    public Transform playerTransform;
    public float heightToPlayer = 2f;
    public Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");

        // Rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Position
        transform.position = playerTransform.position + transform.TransformDirection(cameraOffset);
    }
}
