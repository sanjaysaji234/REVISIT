using UnityEngine;

public class BackgroundRedEffect : MonoBehaviour
{
    public Transform player;
    public Transform monster; // Assigned in Inspector
    public MonsterHealth monsterHealth;
    public PlayerHealth playerHealth;

    [Header("Background Color Settings")]
    public float maxColorDistance = 10f;
    public Color baseColor = Color.white;
    public Color dangerColor = Color.red;

    [Header("Heartbeat Audio Settings")]
    public float maxAudioDistance = 15f;
    public AudioClip heartbeatClip;
    public float minPitch = 0.7f;
    public float maxPitch = 2.0f;
    public float minVolume = 0.05f;
    public float maxVolume = 1.0f;

    private SpriteRenderer spr;
    private AudioSource heartbeatAudio;
    private float volumeVelocity = 0f;
    private float pitchVelocity = 0f;
    private bool isHeartbeatStopped = false;

    public float decreaseSmoothTime = 1.0f;
    public float increaseSpeed = 3.0f;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        if (spr == null)
        {
            Debug.LogError("SpriteRenderer not found on the background!");
        }

        heartbeatAudio = gameObject.AddComponent<AudioSource>();
        heartbeatAudio.clip = heartbeatClip;
        heartbeatAudio.loop = true;
        heartbeatAudio.playOnAwake = false;
        heartbeatAudio.volume = minVolume;
        heartbeatAudio.pitch = minPitch;
        heartbeatAudio.Stop(); // Don't play by default
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned!");
            return;
        }

        // If monster is not assigned or inactive, reset everything
        if (monster == null || !monster.gameObject.activeSelf)
        {
            ResetEffect();
            return;
        }

        // If monster or player is dead, stop effects
        if ((monsterHealth != null && monsterHealth.isDead) || (playerHealth != null && playerHealth.isDead))
        {
            StopHeartbeatEffect();
            return;
        }

        // Calculate distance between player and monster
        float distance = Mathf.Abs(player.position.x - monster.position.x);

        // Normalize values for color and heartbeat
        float colorT = Mathf.Clamp01(1 - (distance / maxColorDistance));
        float audioT = Mathf.Clamp01(1 - (distance / maxAudioDistance));

        // Change background color
        spr.color = Color.Lerp(baseColor, dangerColor, colorT);

        // Adjust heartbeat volume and pitch
        float targetVolume = Mathf.Lerp(minVolume, maxVolume, audioT);
        float targetPitch = Mathf.Lerp(minPitch, maxPitch, audioT);

        heartbeatAudio.volume = (targetVolume > heartbeatAudio.volume)
            ? Mathf.Lerp(heartbeatAudio.volume, targetVolume, Time.deltaTime * increaseSpeed)
            : Mathf.SmoothDamp(heartbeatAudio.volume, targetVolume, ref volumeVelocity, decreaseSmoothTime);

        heartbeatAudio.pitch = (targetPitch > heartbeatAudio.pitch)
            ? Mathf.Lerp(heartbeatAudio.pitch, targetPitch, Time.deltaTime * increaseSpeed)
            : Mathf.SmoothDamp(heartbeatAudio.pitch, targetPitch, ref pitchVelocity, decreaseSmoothTime);

        if (!heartbeatAudio.isPlaying)
        {
            heartbeatAudio.Play();
        }

        isHeartbeatStopped = false;
    }

    private void StopHeartbeatEffect()
    {
        if (isHeartbeatStopped) return;

        ResetEffect();
        isHeartbeatStopped = true;
    }

    private void ResetEffect()
    {
        spr.color = baseColor;
        heartbeatAudio.volume = 0f;
        heartbeatAudio.pitch = minPitch;

        if (heartbeatAudio.isPlaying)
        {
            heartbeatAudio.Stop();
        }
    }
}
