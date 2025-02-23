using UnityEngine;

public class PickUpGuitar : MonoBehaviour
{
    public GameObject interactPrompt;  // "Press E to Interact" prompt
    public bool hasGuitar = false;     // Tracks if the player has the guitar
    private bool isPlayerNear = false;
    public GiveIt giveItScript;        // Reference to GiveIt script

    private SpriteRenderer promptSpriteRenderer;

    void Start()
    {
        if (interactPrompt != null)
        {
            promptSpriteRenderer = interactPrompt.GetComponent<SpriteRenderer>();
            interactPrompt.SetActive(false); // Hide prompt initially
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!giveItScript.haveRose && !hasGuitar)  // Can only pick if no item is held
            {
                hasGuitar = true;
                giveItScript.haveGuitar = true; // Set it in GiveIt script
                gameObject.SetActive(false); // Deactivate guitar after taking it
                Debug.Log("Picked up the Guitar");
            }
            else
            {
                Debug.Log("Get something!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true); // Show prompt when player is near
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false); // Hide prompt when player leaves
            }
        }
    }
}
