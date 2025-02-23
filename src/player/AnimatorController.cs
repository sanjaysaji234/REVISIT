using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private AudioSource Audio;
    private PlayerInput gameInput;
    private Animator animator;
    private PlayerHealth health;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        health = GetComponentInParent<PlayerHealth>();
        gameInput = GetComponentInParent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        bool isMoving = gameInput.moveInput != Vector2.zero;
        bool isWalking = gameInput.movespeed < 1.2f;
        bool isRunning = gameInput.movespeed > 1.2f;
        bool isGrounded = gameInput.onGround;
        bool isJumping = gameInput.isJumping;
        bool isCrouching = gameInput.isCrouch; // New Crouch Animation

        // Set Jump Animation
        animator.SetBool("isJumping", isJumping);

        if (isJumping) return; // Prevents overriding jump animation

        // Set Crouch Animation
        animator.SetBool("isCrouching", isCrouching);

        if (isCrouching) return; // If crouching, other movement animations shouldn't play

        // Set Movement Animations
        animator.SetBool("isWalking", isMoving && isWalking && isGrounded);
        animator.SetBool("isRunning", isMoving && isRunning && isGrounded);

        // Handle Idle State
        if (!isMoving || !isGrounded)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        if (health.isDead)
        {
            Audio.mute = true;
        }
    }
}
