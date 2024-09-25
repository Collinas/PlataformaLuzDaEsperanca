using System.Collections;
using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    [Header("Wall Climbing Settings")]
    [SerializeField] private float wallExitDelay = 0.5f;
    [SerializeField] private float climbingSpeed = 5f;
    [SerializeField] private float jumpForceMultiplier = 0.5f;

    [Header("Raycast Settings")]
    [SerializeField] private float wallDetectionDistance = 0.5f;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float wallDetectionYOffset = 0f;

    [Header("Status Flags")]
    [SerializeField] private bool isClimbing;

    private BaseMovement movementController;
    private Rigidbody2D rigidBody;
    private bool canDetectWall = true;

    private void Start()
    {
        movementController = GetComponent<BaseMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canDetectWall)
        {
            DetectWallAndClimb();
        }

        HandleWallExit();
    }

    private void DetectWallAndClimb()
    {
        Vector2 detectionDirection = new Vector2(transform.localScale.x, 0);
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + wallDetectionYOffset);

        bool isWallDetected = Physics2D.Raycast(rayOrigin, detectionDirection, wallDetectionDistance, wallLayerMask);

        if (isWallDetected && !movementController.isGrounded)
        {
            BeginClimb();
        }
        else
        {
            EndClimb();
        }
    }

    private void BeginClimb()
    {
        isClimbing = true;
        rigidBody.gravityScale = 0;
        movementController.enabled = false;
        HandleClimbingMovement();
    }

    private void EndClimb()
    {
        isClimbing = false;
        rigidBody.gravityScale = 5;
        movementController.enabled = true;
    }

    private void HandleClimbingMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, verticalInput * climbingSpeed);
        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }
    }

    private void HandleWallExit()
    {
        if (Input.GetButtonDown("Jump") && isClimbing)
        {
            JumpOffWall();
            StartCoroutine(TemporarilyDisableWallDetection());
        }
    }

    private void JumpOffWall()
    {
        Vector2 jumpDirection = new Vector2(rigidBody.velocity.x, jumpForceMultiplier);
        rigidBody.velocity = jumpDirection * movementController.jumpForce;

        EndClimb();
    }

    private IEnumerator TemporarilyDisableWallDetection()
    {
        canDetectWall = false;
        yield return new WaitForSeconds(wallExitDelay);
        canDetectWall = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 detectionDirection = new Vector2(transform.localScale.x, 0);
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + wallDetectionYOffset);
        Gizmos.DrawLine(rayOrigin, rayOrigin + detectionDirection * wallDetectionDistance);
    }
}
