using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName; // Set in Inspector or use index
    public float delay = 20f; // Time before switching scenes

    void Start()
    {
        Invoke(nameof(LoadNextScene), delay); // Calls function after 'delay' seconds
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Load scene by name
        }
        else
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Ensure the scene index is valid before loading
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Load next scene by index
            }
            else
            {
                Debug.LogError("Next scene index is out of range! Ensure the scene is in Build Settings.");
            }
        }
    }
}
