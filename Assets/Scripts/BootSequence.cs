using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSequence : MonoBehaviour
{
    [System.Serializable]
    public struct BootLine
    {
        public string text;
        public float delayAfter;
    }

    public BootLine[] bootLines; // Custom lines with delay after each

    public TextMeshProUGUI bootText;
    public float typeSpeed = 0.02f;
    public float delayBeforeStart = 1.0f;
    public float endDelay = 1.5f;
    public string nextSceneName;
    public GameObject typingCursor;
    public float cursorBlinkRate = 0.5f;

    public AudioClip beepSound;
    public AudioClip typeSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TypeBootSequence());

    }

    IEnumerator TypeBootSequence()
    {
        bootText.text = "";
        typingCursor.SetActive(true);
        isTyping = true;
        cursorBlinkRoutine = StartCoroutine(BlinkCursor());

        yield return new WaitForSeconds(delayBeforeStart);

        for (int i = 0; i < bootLines.Length; i++)
        {
            string line = bootLines[i].text;

            foreach (char c in line)
            {
                bootText.text += c;

                if (typeSound != null)
                {
                    audioSource.pitch = Random.Range(0.95f, 1.05f);
                    audioSource.PlayOneShot(typeSound, 0.4f);
                }

                yield return new WaitForSeconds(typeSpeed);
            }

            bootText.text += "\n";

            if (line.Contains("OK") || line.Contains("Booting"))
            {
                if (beepSound != null)
                {
                    audioSource.pitch = 1.0f;
                    audioSource.PlayOneShot(beepSound);
                }
            }

            yield return new WaitForSeconds(bootLines[i].delayAfter);
        }

        isTyping = false;
        typingCursor.SetActive(false);
        if (cursorBlinkRoutine != null) StopCoroutine(cursorBlinkRoutine);

        yield return new WaitForSeconds(endDelay);
        SceneManager.LoadScene(nextSceneName);
    }
    IEnumerator BlinkCursor()
    {
        while (isTyping)
        {
            typingCursor.SetActive(!typingCursor.activeSelf);
            yield return new WaitForSeconds(cursorBlinkRate);
        }
    }
    private Coroutine cursorBlinkRoutine;
    private bool isTyping = false;
}
