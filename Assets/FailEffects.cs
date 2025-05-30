using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FailEffects : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource failAudioSource;

    [Header("Red Flash")]
    public Image redFlashImage;
    public float flashDuration = 0.5f;
    public float flashMaxAlpha = 0.5f;

    [Header("Optional")]
    public bool reloadSceneOnFail = false;
    public float reloadDelay = 1.5f;

    private bool hasFailed = false;

    public void TriggerFailEffects()
    {
        if (hasFailed) return;
        hasFailed = true;

        // Play fail sound
        if (failAudioSource != null)
            failAudioSource.Play();

        // Start red flash
        if (redFlashImage != null)
            StartCoroutine(FlashRed());

        // Reload scene if needed
        if (reloadSceneOnFail)
            Invoke(nameof(ReloadScene), reloadDelay);
    }

    private IEnumerator FlashRed()
    {
        Color color = redFlashImage.color;
        float halfDuration = flashDuration / 2f;

        // Fade in
        float t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, flashMaxAlpha, t / halfDuration);
            redFlashImage.color = color;
            yield return null;
        }

        // Fade out
        t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(flashMaxAlpha, 0f, t / halfDuration);
            redFlashImage.color = color;
            yield return null;
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
