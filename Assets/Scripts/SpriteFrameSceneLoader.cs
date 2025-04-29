using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpriteFrameSceneLoader : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer;  // Assign your player's SpriteRenderer
    public Sprite triggerSprite;                 // The sprite to watch for
    public string sceneToLoad = "NextScene";     // Scene to load
    public float delayBeforeLoad = 3f;           // Delay after sprite is detected
    private bool hasTriggered = false;           // Prevents multiple triggers

    void Update()
    {
        // Check if the sprite matches and we haven't triggered yet
        if (!hasTriggered && targetSpriteRenderer.sprite == triggerSprite)
        {
            hasTriggered = true;
            StartCoroutine(LoadSceneAfterDelay(delayBeforeLoad));
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
