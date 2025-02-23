using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Scenemanager sceneManager;

    private void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        if (sceneManager == null)
        {
            sceneManager =FindFirstObjectByType<Scenemanager>();
        }

        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineEnd;
        }
        else
        {
            Debug.LogError("No PlayableDirector component found!");
        }
    }

    private void OnTimelineEnd(PlayableDirector pd)
    {
        if (sceneManager != null)
        {
            sceneManager.nextLevel();
        }
        else
        {
            Debug.LogError("Scenemanager not found!");
        }
    }
}
