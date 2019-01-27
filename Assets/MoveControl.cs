using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public float acceleration;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * acceleration;
        float moveVertical = Input.GetAxis("Vertical") * acceleration;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.AddForce(movement);
    }
}
