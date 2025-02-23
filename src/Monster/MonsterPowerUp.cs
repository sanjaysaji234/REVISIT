using UnityEngine;
using System.Collections;

public class MonsterPowerUp : MonoBehaviour
{
    public GameObject Monster; // Assign your monster in the Inspector
    public float scaleMultiplier = 1.5f; // How much the monster grows
    public float growthDuration = 0.5f; // Duration of the growth animation
    public int hitCount = 0;
    public MonsterMovement speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Ensure your monster has the tag "Monster"
        {
            hitCount++;
            if (hitCount > 2)
            {
                Debug.Log("Monster entered the power-up zone!");
                StartCoroutine(SmoothGrow(collision.transform, scaleMultiplier, growthDuration));
            }
        }
    }

    private IEnumerator SmoothGrow(Transform target, float multiplier, float duration)
    {
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * multiplier;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale; // Ensure it reaches the final size

        speed.speed = speed.speed + 1.5f;
    }
}
