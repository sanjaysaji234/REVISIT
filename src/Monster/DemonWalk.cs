using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 2f;
    public Transform lightCheck; // Empty GameObject positioned in front of the demon
    public float lightDetectionRange = 0.3f;
    public LayerMask lightLayer; // Assign the Light Layer in the Inspector

    private bool inLight = false;
    private bool isDead = false; // New flag to stop movement

    void Update()
    {
        if (isDead) return; // Stop movement if the monster is dead

        CheckForLight();

        if (!inLight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void CheckForLight()
    {
        RaycastHit2D hit = Physics2D.Raycast(lightCheck.position, Vector2.right, lightDetectionRange, lightLayer);

        // **Draw Ray in Scene View**
        Color rayColor = hit.collider ? Color.red : Color.green; // Red if hitting light, Green if not
        Debug.DrawRay(lightCheck.position, Vector2.right * lightDetectionRange, rayColor);

        inLight = hit.collider != null; // Stops movement if ray hits light
    }

    public void StopMovement() // Call this from MonsterHealth when health reaches 0
    {
        isDead = true;
    }
}