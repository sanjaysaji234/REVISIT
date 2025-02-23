using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject player;
    private PlayerInput playerInput;
    public GameObject playerSprite;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        if (player != null && playerSprite != null)
        {
            audioSource = playerSprite.GetComponent<AudioSource>();
            playerInput = player.GetComponent<PlayerInput>();
            animator = playerSprite.GetComponent<Animator>();

            // Disable player input and audio
            if (playerInput != null)
            {
                audioSource.enabled = false;
                playerInput.enabled = false;
            }

            // Set Animator to "Playback Mode"
            if (animator != null)
            {
                animator.applyRootMotion = false;  // Prevent unwanted movement
                animator.speed = 0;  // Freeze Animator state
            }
        }

        if (timeline != null)
        {
            timeline.stopped += OnCutsceneEnd;
            timeline.Play();
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        // Re-enable player input and audio
        if (playerInput != null)
        {
            audioSource.enabled = true;
            playerInput.enabled = true;
        }

        // Restore Animator to normal
        if (animator != null)
        {
            animator.speed = 1; // Resume normal animation playback
            animator.Rebind();  // Reset all bones and animations properly
            animator.Update(0f); // Apply the reset immediately
        }
    }
}
