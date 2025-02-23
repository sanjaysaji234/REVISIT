using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    public GameObject imageToShow; // Main image when interacting
    public GameObject interactPrompt; // "Press E to Interact" prompt
    private SpriteRenderer promptSpriteRenderer;

    private bool isPlayerNear = false;
    private bool isInteracting = false;
    private PlayerInput playerInput; // Reference to PlayerInput script

    void Start()
    {
        if (imageToShow != null)
        {
            imageToShow.SetActive(false); // Hide interaction UI initially
        }

        if (interactPrompt != null)
        {
            promptSpriteRenderer = interactPrompt.GetComponent<SpriteRenderer>();
            if (promptSpriteRenderer == null)
            {
                Debug.LogError("No SpriteRenderer found on the interact prompt!");
            }
            interactPrompt.SetActive(false); // Hide the prompt initially
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInteracting) // Show image and disable movement
            {
                if (imageToShow != null)
                {
                    imageToShow.SetActive(true);
                }
                if (interactPrompt != null)
                {
                    StartCoroutine(FadeOut(promptSpriteRenderer, interactPrompt));
                }

                // Disable player movement
                playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }

                isInteracting = true;
            }
            else // Hide image and re-enable movement
            {
                if (imageToShow != null)
                {
                    imageToShow.SetActive(false);
                }

                // Re-enable player movement
                if (playerInput != null)
                {
                    playerInput.enabled = true;
                }

                isInteracting = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInteracting)
        {
            isPlayerNear = true;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
                StartCoroutine(FadeIn(promptSpriteRenderer));
            }
        }
    }

    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInteracting)
        {
            isPlayerNear = false;
            if (interactPrompt != null)
            {
                StartCoroutine(FadeOut(promptSpriteRenderer, interactPrompt));
            }
        }
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsed / duration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer, GameObject objToDisable)
    {
        float duration = 0.5f;
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
