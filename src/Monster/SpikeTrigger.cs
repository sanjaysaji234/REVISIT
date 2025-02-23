using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    public MonsterSpike spike; // Reference to the spike object
    private int repeat = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&repeat<=2)
        {
            spike.ActivateSpike(); // Call the spike's movement method
            repeat += 1;
        }
    }
}
