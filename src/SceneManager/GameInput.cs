using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class GameInput : MonoBehaviour
{
    Rigidbody2D rb;
    public float movespeed = 5f;
    [SerializeField] private float jumpForce = 50f;
    public Vector2 moveInput;
    private bool isRight = true;
    public bool onGround = true;
    public bool isJumping = false; // Track jump initiation

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    // Walk
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Run
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movespeed *= 2;
        }
        if (context.canceled)
        {
            movespeed = 5f;
        }
    }

    // Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
            isJumping = true; // Initiate jump
            onGround = false; // Player is no longer on the ground
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

        // Walk and Run
        Vector2 moveDirection = transform.right * moveInput;
        rb.linearVelocity = new Vector2(movespeed * moveDirection.x, rb.linearVelocity.y);
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isRight = !isRight;
    }


    private void CheckGrounded()
    {
        // Define box cast parameters
        Vector2 origin = transform.position; // Center of the box
        Vector2 size = new Vector2(2f, 0.1f); // Width and height of the box (adjust height as needed)
        float angle = 0f; // Angle of the box
        Vector2 direction = Vector2.down; // Direction of the box cast
        float rayLength = 1.7f; // Distance of the box cast
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // Ground layer mask

        // Perform box cast
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, rayLength, groundLayer);

        // Debug box to visualize in Scene view
        Debug.DrawRay(origin, direction * rayLength, Color.yellow); // Center line
        Debug.DrawLine(origin + new Vector2(-size.x / 2, 0), origin + new Vector2(-size.x / 2, 0) + direction * rayLength, Color.red); // Left line
        Debug.DrawLine(origin + new Vector2(size.x / 2, 0), origin + new Vector2(size.x / 2, 0) + direction * rayLength, Color.blue); // Right line

        bool wasOnGround = onGround;

        // Check if the box hits the ground
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            onGround = true;

            // Only reset isJumping when player has actually landed
            if (!wasOnGround && rb.linearVelocity.y <= 6)
            {
                // Ensure we reset isJumping only when the player has landed (velocity is downward)
                isJumping = false; // Player has landed
            }
        }
        else
        {
            onGround = false;
        }
    }

}
