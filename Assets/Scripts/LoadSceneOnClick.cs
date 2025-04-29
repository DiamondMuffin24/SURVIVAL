using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneToLoad = "NextScene";  // Name of the scene to load

    // This method can be linked to a UI Button's OnClick event
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
