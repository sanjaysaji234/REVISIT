using UnityEngine;
using System.Collections;

public class FollowPlayerWhenCrouching : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    private PlayerInput playerInput;

    public GameObject crouchPrompt; // Assign the "Hold Ctrl to Crouch" GameObject
    private SpriteRenderer promptSpriteRenderer;
    private bool hasTriggered = false;

    private void Start()
    {
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        if (crouchPrompt != null)
        {
            promptSpriteRenderer = crouchPrompt.GetComponent<SpriteRenderer>();
            if (promptSpriteRenderer == null)
            {
                Debug.LogError("No SpriteRenderer found on the crouch prompt!");
            }
            crouchPrompt.SetActive(false); // Hide prompt initially
        }
    }

    private void Update()
    {
        if (playerInput != null && playerInput.isCrouch)
        {
            // Only change the X position if the player is moving
            float newX = player.position.x;

            if (playerInput.moveInput.x != 0) // If the player is moving
            {
                // Update X position based on player movement
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else
            {
                // Keep the object at the offset when the player stops moving
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Ensure it only happens once

            if (crouchPrompt != null)
            {
                crouchPrompt.SetActive(true);
                StartCoroutine(FadeIn(promptSpriteRenderer));
            }
        }
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsed / duration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f); // Show prompt for 2 seconds
        StartCoroutine(FadeOut(spriteRenderer, crouchPrompt));
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer, GameObject objToDisable)
    {
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsed / duration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(1, 1, 1, 0);
        objToDisable.SetActive(false);
    }
}
