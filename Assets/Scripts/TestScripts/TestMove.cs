using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    // Variables
    public float speed = 0f;
    public Rigidbody player;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float getY = player.velocity.y;
        Vector3 move = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        player.velocity = move * speed;
        player.velocity = new Vector3(player.velocity.x, getY, player.velocity.z);
    }
}
