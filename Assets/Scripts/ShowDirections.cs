using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDirections : MonoBehaviour
{
        // Receive arrows
        public GameObject UpArrow, DownArrow, LeftArrow, RightArrow;
 
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
                // Display arrows      
                float moveX = Input.GetAxis("Horizontal");
                float moveZ = Input.GetAxis("Vertical");

                UpArrow.SetActive(moveZ > 0f);
                DownArrow.SetActive(moveZ < 0f);
                RightArrow.SetActive(moveX > 0f);
                LeftArrow.SetActive(moveX < 0F);
        }
}
