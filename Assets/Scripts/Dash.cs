using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Layer Settings")]
    [SerializeField] private LayerMask trapLayer;

    private BaseMovement baseMovement;
    private Rigidbody2D rb;
    [SerializeField] public bool canDash = true;
    private bool isDashing = false;

    private void Start()
    {
        baseMovement = GetComponent<BaseMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canDash && baseMovement.IsMoving)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        StartDash();

        yield return new WaitForSeconds(dashDuration);

        EndDash();

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void StartDash()
    {
        canDash = false;
        isDashing = true;

        float dashDirection = Mathf.Sign(rb.velocity.x);
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0);

        rb.gravityScale = 0;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Traps"), true);
        baseMovement.enabled = false;
    }

    private void EndDash()
    {
        rb.gravityScale = 5;
        rb.velocity = Vector2.zero;
        isDashing = false;

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Traps"), false);
        baseMovement.enabled = true;
    }
}
