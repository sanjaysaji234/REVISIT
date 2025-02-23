using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene4to5 : MonoBehaviour
{
    public Scenemanager SceneManager;
    public GiveIt GiveIt;
    public bool guitarplayed;

    


    private void Update()
    {
        if (GiveIt.RoseDone &&guitarplayed)
        {
            SceneManager.nextLevel();
        }
    }
}
