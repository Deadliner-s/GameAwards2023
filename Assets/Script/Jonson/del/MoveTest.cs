using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed = 10f; // The speed of movement

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical input axes (WASD keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction to move towards
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        // Normalize the direction vector (optional)
        direction.Normalize();


        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation *= Quaternion.Euler(-speed, 0, 0);   
        }
            // Move the character in the calculated direction
            transform.position += direction * speed * Time.deltaTime;
    }
}
