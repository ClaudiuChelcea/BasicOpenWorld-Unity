using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    // Get the player's transform
    public Transform getPlayer;
    float x_movement = 0f;
    float z_movement = 0f;
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;
    public float distanceToTarget = 0f;
    public float offsetRightToTarget = 0f;
    public float offsetUpToTarget = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get stats
        x_movement += Input.GetAxis("Mouse X");
        z_movement -= Input.GetAxis("Mouse Y");

        // Position
        transform.position = getPlayer.position - transform.forward * distanceToTarget - transform.right * offsetRightToTarget + transform.up * offsetUpToTarget;

        // Rotation
        transform.rotation = Quaternion.Euler(z_movement * sensitivityY, x_movement * sensitivityX, 0);
    
    }
}
