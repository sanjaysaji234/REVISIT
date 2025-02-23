using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    public GameObject keyCollectedImage; // New image that fades in and out
    private bool isPlayerNear = false;
    private SpriteRenderer keySpriteRenderer;
    private SpriteRenderer keyCollectedSpriteRenderer;
    public bool hasKey = false;

    private void Start()
    {
        if (keyCollectedImage != null)
        {
            keyCollectedSpriteRenderer = keyCollectedImage.GetComponent<SpriteRenderer>();
            keyCollectedImage.SetActive(false);
        }
        keySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.K))
        {
            hasKey = true;
            StartCoroutine(FadeInAndOutKeyCollectedImage());
            if (keySpriteRenderer != null)
            {
                keySpriteRenderer.enabled = false; // Disable only the sprite renderer
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    IEnumerator FadeInAndOutKeyCollectedImage()
    {
        if (keyCollectedImage != null && keyCollectedSpriteRenderer != null)
        {
            keyCollectedImage.SetActive(true);
            float duration = 1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, elapsed / duration);
                keyCollectedSpriteRenderer.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, elapsed / duration);
                keyCollectedSpriteRenderer.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            keyCollectedImage.SetActive(false);
        }
    }
}


