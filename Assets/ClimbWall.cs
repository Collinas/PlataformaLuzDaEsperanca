using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    //improve usage focusing on readability
    [Header("Settings")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] LayerMask wallLayer;

    [Header("Checks")]
    [SerializeField] private bool isClimb;

    private BaseMovement baseMovement;

    private void Start()
    {
        baseMovement = GetComponent<BaseMovement>();
    }

    private void Update()
    {
        CheckClimbWall();
        ExitWallClimb();
    }

    private void CheckClimbWall()
    {
        //change for box raycast
        isClimb = Physics2D.Raycast(transform.position, Vector2.down, wallCheckDistance, wallLayer);

        if (isClimb && !baseMovement.isGrounded)
            HandleClimb();
    }

    private void HandleClimb()
    {
        //climb logic towards the climb wall
    }

    private void ExitWallClimb()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isClimb) 
            {
                //change the jump to go to the wall opposite direction and slightly up (half jumpforce)
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, baseMovement.jumpForce);

                //add logic to remove the handleclimb logic when exitwallclimb
                isClimb = false;
            }
        }
    }
}
