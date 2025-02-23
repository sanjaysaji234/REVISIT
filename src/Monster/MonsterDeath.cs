using JetBrains.Annotations;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead=false;
    private CapsuleCollider2D collider2d;
    private Rigidbody2D rigidbody2d;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth; // Initialize health
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light")) // Light collider must have this tag
        {
            TakeDamage(25);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Monster took damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        rigidbody2d.gravityScale = 0;
        collider2d.enabled = false;
        isDead = true;
        MonsterMovement movement = GetComponent<MonsterMovement>();
        if (movement != null)
        {
            movement.StopMovement(); // Stops movement
        }
    }

}
