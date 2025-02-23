using UnityEngine;

public class HeartbeatEffect : MonoBehaviour
{
    public Transform enemy;  // Assign the enemy GameObject
    public AudioSource heartbeatAudio;  // Assign the heartbeat AudioSource
    public float minDistance = 5f;  // Distance at which heartbeat starts increasing
    public float maxDistance = 20f; // Distance at which heartbeat is at its slowest
    public float minPitch = 0.8f;   // Slowest heartbeat pitch
    public float maxPitch = 2.0f;   // Fastest heartbeat pitch
    public float minVolume = 0.1f;  // Lowest volume
    public float maxVolume = 1.0f;  // Highest volume

    void Update()
    {
        if (enemy == null || heartbeatAudio == null) return;

        // Calculate distance between player and enemy
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Normalize the distance between min and max range (clamp to avoid errors)
        float t = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // Adjust pitch and volume based on distance
        heartbeatAudio.pitch = Mathf.Lerp(minPitch, maxPitch, t);
        heartbeatAudio.volume = Mathf.Lerp(minVolume, maxVolume, t);
    }
}
