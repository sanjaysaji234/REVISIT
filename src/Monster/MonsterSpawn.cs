using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the monster prefab in the Inspector
    private bool hasSpawned = false; // Ensure it spawns only once

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!hasSpawned && other.CompareTag("Player"))
        {

            hasSpawned = true;

            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(true);

                BackgroundRedEffect bgEffect = Object.FindFirstObjectByType<BackgroundRedEffect>();
                if (bgEffect != null)
                {
                    bgEffect.monster = objectToSpawn.transform;
                }
                
            }
            
        }
    }

}
