using UnityEngine;

public class GamePauseOnCollision : MonoBehaviour

{
    public PlayerHealth playerhealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerInput script to check if the player is crouching
            PlayerInput playerInput = collision.GetComponent<PlayerInput>();

            if (playerInput != null && !playerInput.isCrouch)
            {
                playerhealth.health = 0;
            }
        }
    }
}
