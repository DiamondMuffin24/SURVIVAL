using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class FLBar : MonoBehaviour
{
    public UIDocument uiDocument;
    public float loadDuration = 5f; // Duration of the fake loading in seconds

    private ProgressBar loadingBar;

    private void OnEnable()
    {
        // Get the root of the UI document
        VisualElement root = uiDocument.rootVisualElement;

        // Find the Progress Bar by name
        loadingBar = root.Q<ProgressBar>("loadingBar");

        // Start the fake loading coroutine
        StartCoroutine(FakeLoad());
    }

    private IEnumerator FakeLoad()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadDuration) * 100f;
            loadingBar.value = progress;
            yield return null;
        }

        // Loading complete
        Debug.Log("Loading Complete!");
        // Optionally, trigger an event or load a new scene here
    }
}