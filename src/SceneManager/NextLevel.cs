using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Scenemanager sceneManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Finish")) // Check if collided object has "Player" tag
        {
            sceneManager.nextLevel();
        }
    }
}
