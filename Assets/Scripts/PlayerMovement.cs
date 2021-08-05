using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public  float player_speed = 0f;
    public Rigidbody player;
    public Transform cameraTransform;

    // Start is called before the first frame updat
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        float x_Axis = Input.GetAxis("Horizontal");
        float z_Axis = Input.GetAxis("Vertical");
        Vector3 moveDirection = cameraTransform.forward * z_Axis + cameraTransform.right * x_Axis;

        // Move player
        var get_actual_velocity = player.velocity.y;
        player.velocity = moveDirection * player_speed; // Update horizontal movement
        player.velocity = new Vector3(player.velocity.x, get_actual_velocity, player.velocity.z); // Update vertical movement

        // Fix rotation
        player.rotation = cameraTransform.rotation * Quaternion.Euler(0,cameraTransform.rotation.y + 90f,0);
    }
}
