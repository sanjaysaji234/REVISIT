using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Scenemanager sceneManager;

    private void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (sceneManager == null)
        {
            sceneManager = Object.FindFirstObjectByType<Scenemanager>();
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("No VideoPlayer component found!");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (sceneManager != null&& SceneManager.GetActiveScene().buildIndex<11)
        {
            sceneManager.nextLevel();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
