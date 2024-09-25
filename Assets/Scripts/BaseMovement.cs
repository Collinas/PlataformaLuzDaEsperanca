using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] public float jumpForce = 10f;

    [Header("Raycast Settings")]
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Status Flags")]
    [SerializeField] public bool isGrounded;
    [SerializeField] private bool isSprinting;
    public bool IsMoving { get; private set; }

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGroundStatus();
        HandleMovement();
        HandleJump();
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded)
        {
            isSprinting = Input.GetKey(KeyCode.LeftShift);
        }
    }

    public void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
        IsMoving = moveInput != 0;
    }

    public void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
