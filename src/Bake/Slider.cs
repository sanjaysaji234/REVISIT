using UnityEngine;

public class SliderInteraction : MonoBehaviour
{
    public float speed = 2.0f;
    public float startY = -5.0f;
    public float endY = 5.0f;
    public GameObject[] objectsToActivate; // Assign objects that should be activated
    private bool canInteract=false;
    private bool interacted=false;
    public Scenemanager scenemanager;

    private int activeObjectIndex = -1;

    void Start()
    {
        Debug.Log("Script started");
        // Ensure all objects start as inactive
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&canInteract&&!interacted)
        {
            ActivateNextObject();
        }
        else if(Input.GetKeyDown(KeyCode.E) && !canInteract)
        {
            scenemanager.gameOver();
        }
        MoveInLoop();
    }

    void MoveInLoop()
    {
        float newY = Mathf.PingPong(Time.time * speed, endY - startY) + startY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interacted = false;
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canInteract=false;
    }

    void ActivateNextObject()
    {
        if (activeObjectIndex + 1 < objectsToActivate.Length)
        {
            activeObjectIndex++;
            objectsToActivate[activeObjectIndex].SetActive(true);
        }
        interacted = true;
    }
}
