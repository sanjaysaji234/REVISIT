using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Scenemanager sceneManager;
    public int health = 100;
    public bool isDead = false;
    public MonsterPowerUp powerUp;
    private AudioSource audioSource;
    public AudioClip hurtSound;
    private PlayerInput playerInput;
    private Collider2D playerCollider;

    private void Start()
    {
        // Assign AudioSource if not set in Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Ensure powerUp is assigned
        if (powerUp == null)
        {
            powerUp = GetComponent<MonsterPowerUp>();
        }

        // Get player collider for collision handling
        playerCollider = GetComponent<Collider2D>();

        // Get player input
        playerInput = GetComponent<PlayerInput>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            Debug.Log("Damage Taken");
            health -= damage;
            Debug.Log("Player hit! Health: " + health);

            // Play hurt sound if assigned
            if (hurtSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hurtSound);
            }

            if (health <= 0)
            {
                isDead = true;
                sceneManager.gameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (playerInput != null && playerInput.isCrouch)
            {
                // Ignore collision if player is crouching
                Physics2D.IgnoreCollision(playerCollider, collision, true);
                return;
            }
            else if (!playerInput.isCrouch)
            {
                TakeDamage(100);
            }

            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (powerUp == null || powerUp.hitCount < 2)
            {
                TakeDamage(100);
            }
            else
            {
                sceneManager.nextLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Re-enable collision when the player exits the enemy's trigger zone
            Physics2D.IgnoreCollision(playerCollider, collision, false);
        }
    }
}
