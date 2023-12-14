using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    // Static instance of the SceneLoader which allows it to be accessed by any other script.
    public static SceneBehaviour Instance { get; private set; }

    private void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            // if not, set the instance to this
            Instance = this;
            // Do not destroy this instance when reloading scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If instance already exists and it's not this, then destroy this to enforce the singleton pattern
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
