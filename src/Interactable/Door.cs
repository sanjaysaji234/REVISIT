using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject closedDoor;  // Closed door GameObject
    public GameObject openDoor;    // Open door GameObject
    public GameObject interactPrompt; // "Press E to Open" UI
    public GameObject findKeyPrompt; // "Find Key" UI
    private SpriteRenderer interactPromptRenderer;
    private SpriteRenderer findKeyPromptRenderer;
    private bool isPlayerNear = false;
    public Key Key;
    private void Start()
    {
        if (openDoor != null)
        {
            openDoor.SetActive(false); // Hide the open door initially
        }
        if (interactPrompt != null)
        {
            interactPromptRenderer = interactPrompt.GetComponent<SpriteRenderer>();
            interactPrompt.SetActive(false); // Hide the interact prompt initially
        }
        if (findKeyPrompt != null)
        {
            findKeyPromptRenderer = findKeyPrompt.GetComponent<SpriteRenderer>();
            findKeyPrompt.SetActive(false); // Hide the find key prompt initially
        }
    }

    private void Update()
    {
        if (isPlayerNear)
        {
            if (Key.hasKey)
            {
                findKeyPrompt.SetActive(false);
                interactPrompt.SetActive(true);
                StartCoroutine(FadeIn(interactPromptRenderer));
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenDoor();
                }
            }
            else
            {
                interactPrompt.SetActive(false);
                findKeyPrompt.SetActive(true);
                StartCoroutine(FadeIn(findKeyPromptRenderer));
            }
        }
        else
        {
            if (interactPrompt.activeSelf)
            {
                StartCoroutine(FadeOut(interactPromptRenderer, interactPrompt));
            }
            if (findKeyPrompt.activeSelf)
            {
                StartCoroutine(FadeOut(findKeyPromptRenderer, findKeyPrompt));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private void OpenDoor()
    {
        if (closedDoor != null)
        {
            closedDoor.SetActive(false); // Hide the closed door
        }

        if (openDoor != null)
        {
            openDoor.SetActive(true); // Show the open door
        }

        StartCoroutine(FadeOut(interactPromptRenderer, interactPrompt));
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

