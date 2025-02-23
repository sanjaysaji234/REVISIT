using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed = 2f;
    private float walkspeed;
    public Vector2 moveInput;
    private bool isRight = true;
    public bool onGround = true;
    public bool isJumping = false;
    [SerializeField] private float jumpForce = 50f;
    public bool isCrouch = false;
    private bool canCrouch = false;
    [SerializeField] private float raycastDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    private bool isBlocked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckForWall();
        HandleMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started&&!isCrouch)
        {
            walkspeed = movespeed;
            movespeed *= 1.8f; 
        }
        if (context.canceled)
        {
            movespeed = walkspeed;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && onGround && !isCrouch)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            onGround = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started && onGround && canCrouch)
        {
            isCrouch = true;
        }
        if (context.canceled)
        {
            isCrouch = false;
        }
    }

    private void HandleMovement()
    {
        if (moveInput.x > 0 && !isRight)
        {
            Flip();
        }
        if (moveInput.x < 0 && isRight)
        {
            Flip();
        }

        if (!isBlocked)
        {
            Vector2 moveDirection = transform.right * moveInput;
            rb.linearVelocity = new Vector2(movespeed * moveDirection.x, rb.linearVelocity.y);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isRight = !isRight;
    }

    private void CheckForWall()
    {
        Vector2 rayOrigin = (Vector2)transform.position + Vector2.down * 0.5f; // Offset downward
        Vector2 direction = isRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, raycastDistance, groundLayer);

        // Draw the ray in the Scene view
        Debug.DrawRay(rayOrigin, direction * raycastDistance, Color.red);

        isBlocked = hit.collider != null;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            
            isJumping = false;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide"))
        {
            canCrouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide"))
        {
            canCrouch = false;
            isCrouch = false;
        }
    }
}
