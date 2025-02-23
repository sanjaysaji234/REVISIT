using UnityEngine;

public class GiveIt : MonoBehaviour
{
    public GameObject guitar;      // The guitar object
    public GameObject rose;        // The rose object
    public GameObject givenRose;
    public GameObject roseDialogue;
    public GameObject givenGuitar;
    public GameObject interactPrompt;  // "Press E to Interact" prompt
    public GameObject nothingDialogue;

    private SpriteRenderer givenRoseRenderer;
    public bool haveGuitar = false;    // Tracks if player has the guitar
    public bool haveRose = false;      // Tracks if player has the rose
    public bool RoseDone = false;
    public bool GuitDone = false;
   

    private bool isPlayerNear = false;
    private SpriteRenderer promptSpriteRenderer;

    void Start()
    {
        roseDialogue.SetActive(false);
        givenRoseRenderer= givenRose.GetComponent<SpriteRenderer>();
        if (interactPrompt != null)
        {
            promptSpriteRenderer = interactPrompt.GetComponent<SpriteRenderer>();
            interactPrompt.SetActive(false); // Hide prompt initially
        }

        // Ensure the guitar is initially disabled
        guitar.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (haveRose )
            {
                // If the player has the rose, give the guitar and mark rose as given
                haveRose = false;
                nothingDialogue.SetActive(false);
                givenRoseRenderer.enabled = true;
                roseDialogue.SetActive (true);
                Debug.Log("Rose Given");
                RoseDone = true;
            }
            else if (haveGuitar)
            {
                // If the player has the guitar, give the rose and mark guitar as given
                haveGuitar = false;
                givenGuitar.SetActive(true);
                roseDialogue.SetActive(false);
                Debug.Log("Guitar Given");
                GuitDone = true; 
            }
            else
            {
                nothingDialogue.SetActive(true);
                Debug.Log("Get something!");
            }
            
            interactPrompt.SetActive(false); // Hide prompt after interaction
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
