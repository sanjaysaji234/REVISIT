using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void newGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1); 
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    
}
