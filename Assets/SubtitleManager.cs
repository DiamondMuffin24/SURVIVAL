using UnityEngine;
using UnityEngine.UI;
using TMPro; // if using TextMeshPro

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;

    public TextMeshProUGUI subtitleText; // Assign in Inspector
    private Coroutine currentCoroutine;

    private void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowSubtitle(string text, float duration)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ShowSubtitleCoroutine(text, duration));
    }

    private System.Collections.IEnumerator ShowSubtitleCoroutine(string text, float duration)
    {
        subtitleText.text = text;
        subtitleText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        subtitleText.gameObject.SetActive(false);
        subtitleText.text = "";
    }
}
