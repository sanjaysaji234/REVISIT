using UnityEngine;
using UnityEngine.Rendering.Universal; // For 2D Lights
using System.Collections;

public class LightSwitch : MonoBehaviour
{
    public Light2D lightSource; // 2D URP Light
    public Collider2D[] colliders; // Colliders to enable when light is on
    public float lightDuration = 3f; // Light stays on for this duration
    public GameObject interactPrompt; // "Press E to Interact" GameObject (Sprite)
    public float fadeDuration = 0.5f; // Duration for fading effect

    private bool isOn = false; // Light state
    private bool playerNearby = false; // Is the player near?
    private SpriteRenderer promptSprite; // For fading effect

    void Start()
    {
        SetLightState(false); // Ensure light is off at start

        if (interactPrompt != null)
        {
            promptSprite = interactPrompt.GetComponent<SpriteRenderer>();
            if (promptSprite != null)
            {
                promptSprite.color = new Color(1, 1, 1, 0); // Start with prompt invisible
            }
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isOn) // Press 'F' to interact
        {
            StartCoroutine(TurnOnLight());
            StartCoroutine(FadeOutPrompt()); // Hide prompt after interaction
        }
    }

    IEnumerator TurnOnLight()
    {
        SetLightState(true);
        yield return new WaitForSeconds(lightDuration);
        SetLightState(false);
    }

    void SetLightState(bool state)
    {
        isOn = state;
        lightSource.enabled = state; // Turn 2D light on/off

        // Enable/Disable colliders
        foreach (Collider2D col in colliders)
        {
            col.enabled = state;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
                StartCoroutine(FadeInPrompt()); // Smooth fade-in when player enters
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            StartCoroutine(FadeOutPrompt()); // Smooth fade-out when player leaves
        }
    }

    IEnumerator FadeInPrompt()
    {
        float elapsedTime = 0f;
        Color color = promptSprite.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            promptSprite.color = color;
            yield return null;
        }

        color.a = 1;
        promptSprite.color = color;
    }

    IEnumerator FadeOutPrompt()
    {
        float elapsedTime = 0f;
        Color color = promptSprite.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            promptSprite.color = color;
            yield return null;
        }

        color.a = 0;
        promptSprite.color = color;
        interactPrompt.SetActive(false);
    }
}
