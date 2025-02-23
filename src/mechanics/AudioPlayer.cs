using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Assign in Inspector
    public AudioClip walkClip; // Assign walking sound in Inspector
    public AudioClip runClip; // Assign running sound in Inspector

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (footstepAudioSource == null)
        {
            footstepAudioSource = gameObject.AddComponent<AudioSource>();
        }

        footstepAudioSource.loop = true; // Loop the audio while moving
        footstepAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping"); // Check if jumping

        if ((isWalking || isRunning) && !isJumping) // Stop footsteps while jumping
        {
            PlayFootstep(isRunning);
        }
        else
        {
            StopFootstep();
        }
    }

    private void PlayFootstep(bool isRunning)
    {
        AudioClip currentClip = isRunning ? runClip : walkClip;

        if (footstepAudioSource.clip != currentClip)
        {
            footstepAudioSource.clip = currentClip;
            footstepAudioSource.Play();
        }

        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }

    private void StopFootstep()
    {
        if (footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop(); // Stops the sound instantly when movement stops
        }
    }
}
