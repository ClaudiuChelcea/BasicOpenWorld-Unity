using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    // Variables
    bool is_left = true;
    bool start_vibrate = false;

    // Start is called before the first frame update
    void Start()
    {
        start_vibrate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 2)
        {
            start_vibrate = true;
        }
        else
        {
            start_vibrate = false;
        }

        if(start_vibrate)
        {
            if(is_left == true) {
                // Move right
                is_left = false;
                transform.position -= new Vector3(22, 0, 0) * Time.deltaTime;
                new WaitForSeconds(2);
            }
            else { // Move left
                is_left = true;
                transform.position += new Vector3(22, 0, 0) * Time.deltaTime;
                new WaitForSeconds(2);
            }
        }
    }
}
