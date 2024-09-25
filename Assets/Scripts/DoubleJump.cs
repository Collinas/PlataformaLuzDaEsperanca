using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private BaseMovement baseMovement;
    private bool canDoubleJump;

    void Start()
    {
        baseMovement = GetComponent<BaseMovement>();
    }

    void Update()
    {
        if (baseMovement.isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (baseMovement.isGrounded)
            {
                baseMovement.HandleJump();
            }
            else if (canDoubleJump)
            {
                HandleDoubleJump();
                canDoubleJump = false;
            }
        }
    }

    void HandleDoubleJump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, baseMovement.jumpForce);
    }
}
