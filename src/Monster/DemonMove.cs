using UnityEngine;
using System.Collections;
using Cinemachine;

public class DemonMove : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f; // Distance to move left and back
    [SerializeField] private float moveSpeed = 2f;    // Speed of movement
    [SerializeField] private float moveDelay = 2f;    // Time before moving back
    [SerializeField] private float shakeIntensity = 2f; // Camera shake intensity
    [SerializeField] private float shakeDuration = 0.3f; // How long the shake lasts
    [SerializeField] private AudioSource audioSource; // AudioSource to play sound

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool hasMoved = false; // Ensures this happens only once

    private CinemachineImpulseSource impulseSource; // For Camera Shake

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x - moveDistance, originalPosition.y, originalPosition.z);
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasMoved)
        {
            hasMoved = true;

            // Play the audio when the object comes in
            if (audioSource != null)
            {
                audioSource.Play();
            }

            StartCoroutine(MoveObject(targetPosition, moveSpeed, true));
        }
    }

    private IEnumerator MoveObject(Vector3 destination, float speed, bool moveBack)
    {
        float step = speed * Time.deltaTime;

        // **Trigger Camera Shake**
        ShakeCamera();

        // Move smoothly to the target position
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            yield return null; // Wait until the next frame
        }

        if (moveBack)
        {
            yield return new WaitForSeconds(moveDelay);
            StartCoroutine(MoveBackAndDestroy());
        }
    }

    private IEnumerator MoveBackAndDestroy()
    {
        float step = moveSpeed * Time.deltaTime;

        // Move smoothly back to original position
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);
            yield return null;
        }

        // Destroy the object after it returns
        Destroy(gameObject);
    }

    private void ShakeCamera()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulseWithForce(shakeIntensity);
        }
    }
}
