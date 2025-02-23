using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    public AudioClip hitSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;
    private bool hasPlayed = false; // Flag to track if the sound has played

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // Ensure it doesn't play on start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayed && other.CompareTag("Enemy")) // Check if it's an enemy and sound hasn't played
        {
            PlayHitSound();
            hasPlayed = true; // Prevents further triggers
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
