using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform upperFloorPosition; // Assign in Inspector
    [SerializeField] private Transform lowerFloorPosition; // Assign in Inspector
    [SerializeField] private float teleportOffsetX = 1.5f; // Offset for teleporting
    [SerializeField] private float teleportDelay = 1.5f; // Delay before teleporting
    private float realGravity;

    private Rigidbody2D rb;
    private bool canTeleport = true; // Prevent multiple triggers

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport") && canTeleport)
        {
            StartCoroutine(TeleportAfterDelay());
        }
    }

    private IEnumerator TeleportAfterDelay()
    {
        canTeleport = false; // Prevent multiple triggers

        // **Disable Gravity & Movement to Prevent Falling**
        rb.linearVelocity = Vector2.zero;
        realGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(teleportDelay); // Wait 1.5 seconds

        Vector3 newPosition;
        if (transform.position.y < (upperFloorPosition.position.y + lowerFloorPosition.position.y) / 2)
        {
            // **Teleporting from bottom to top (spawn a bit RIGHT)**
            newPosition = new Vector3(upperFloorPosition.position.x + teleportOffsetX, upperFloorPosition.position.y, transform.position.z);
        }
        else
        {
            // **Teleporting from top to bottom (spawn a bit LEFT)**
            newPosition = new Vector3(lowerFloorPosition.position.x - teleportOffsetX, lowerFloorPosition.position.y, transform.position.z);
        }

        transform.position = newPosition;

        // **Re-enable Gravity & Movement**
        rb.gravityScale = realGravity;

        Invoke(nameof(ResetTeleport), 0.5f); // Small cooldown before allowing teleport again
    }

    private void ResetTeleport()
    {
        canTeleport = true;
    }
}
