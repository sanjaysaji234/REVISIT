using UnityEngine;

public class DemonAnimator : MonoBehaviour
{
    private Animator animator;
    private MonsterHealth health;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponentInParent<MonsterHealth>();
    }
    private void Update()
    {
        if (health.currentHealth ==75)
        {
            animator.SetBool("Walk2",true);
        }
        else if (health.currentHealth == 50)
        {
            animator.SetBool("Walk3", true);
        }

        else if (health.currentHealth == 25)
        {
            animator.SetBool("Walk4", true);
        }
        else if(health.currentHealth == 0)
        {
            animator.SetBool("Death", true);
        }
    }
}


