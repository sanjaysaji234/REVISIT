using UnityEngine;
using System.Collections;

public class MonsterSpike : MonoBehaviour
{
    public float initialMoveDistance = 0.5f; // Small instant movement
    public float fullMoveDistance = 2f; // Full movement distance
    public float midDelay = 1f; // Delay after reaching mid position
    public float returnDelay = 0.5f; // Time before returning to initial position
    public float moveSpeed = 10f; // Speed of smooth movement

    private Vector3 initialPosition;
    private Vector3 midPosition;
    private Vector3 fullPosition;
    private bool hasHitPlayer = false; // Ensures player is hit only once per activation
    private bool isActivated = false; // Prevents multiple activations

    private void Start()
    {
        initialPosition = transform.position;
        midPosition = initialPosition + new Vector3(0, initialMoveDistance, 0);
        fullPosition = initialPosition + new Vector3(0, fullMoveDistance, 0);
    }

    public void ActivateSpike()
    {
        if (!isActivated) // Prevents reactivation while in motion
        {
            isActivated = true;
            StartCoroutine(MoveSpikeSequence());
        }
    }

    private IEnumerator MoveSpikeSequence()
    {
        // Step 1: Move up slightly instantly
        transform.position = midPosition;

        // Step 2: Wait before moving fully
        yield return new WaitForSeconds(midDelay);

        // Step 3: Move smoothly to full position
        yield return StartCoroutine(SmoothMove(transform.position, fullPosition, moveSpeed));

        // Step 4: Wait before returning
        yield return new WaitForSeconds(returnDelay);

        // Step 5: Move smoothly back to the initial position
        yield return StartCoroutine(SmoothMove(transform.position, initialPosition, moveSpeed));

        // Step 6: Reset hit detection only after full cycle is complete
        hasHitPlayer = false;
        isActivated = false;
    }

    private IEnumerator SmoothMove(Vector3 start, Vector3 end, float speed)
    {
        float duration = 0.2f; // Shorter duration for faster movement
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.position = end; // Ensure exact final position
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHitPlayer)
        {
            Debug.Log("hitCollider");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null )
            {
                playerHealth.TakeDamage(25);
                hasHitPlayer = true; // Player gets hit only once per spike activation
            }
        }
    }
}
