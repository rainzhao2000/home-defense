using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 100;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement = new Vector2(0, speed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement = new Vector2(0, -speed);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement = new Vector2(-speed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = new Vector2(speed, 0);
        }
        else
        {
            movement = new Vector2(0, 0);
        }
        rb.AddForce(movement);
    }
}
