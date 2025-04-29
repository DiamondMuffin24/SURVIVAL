using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SystemReboot : MonoBehaviour
{
    public TextMeshProUGUI terminalText;
    public AudioSource glitchSound;
    public AudioSource bootSound;
    public float lineDelay = 0.6f;
    public Image screenFlash;
    public float flashDuration = 0.3f;
    public Transform cameraTransform;   // Drag your main camera here in Inspector
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private string[] bootLines = new string[]
    {
        "[ REBOOTING SYSTEM... ]",
        "> Neural core integrity: STABLE",
        "> Retinal HUD interface: ONLINE",
        "> Motor cortex override: SYNCHRONIZED",
        "> reality cortex: AWAKE",
        "> Memory cache recovery: 92%",
        "> Iteration Storage: PULLING",
        "> Deploying consciousness module...",
        "> SYSTEM ONLINE."
    };

    void OnEnable()
    {
        StartCoroutine(RebootSequence());
    }

    IEnumerator RebootSequence()
    {
        terminalText.text = "";

        foreach (string line in bootLines)
        {
            string outputLine = line;

            // Randomize percentage if line contains "Memory cache recovery"
            if (line.Contains("Memory cache recovery"))
            {
                int percent = Random.Range(65, 99);
                outputLine = $"> Memory cache recovery: {percent}%";
            }

            terminalText.text += outputLine + "\n";

            if (glitchSound != null)
                glitchSound.Play();

            yield return new WaitForSeconds(lineDelay);
        }

        if (bootSound != null)
            bootSound.Play();

        StartCoroutine(ScreenFlashEffect());
        StartCoroutine(CameraShake());

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
    IEnumerator CameraShake()
    {
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
    IEnumerator ScreenFlashEffect()
    {
        float elapsed = 0f;
        Color c = screenFlash.color;
        c.a = 1f;
        screenFlash.color = c;

        // Fade out over time
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
            screenFlash.color = c;
            yield return null;
        }

        c.a = 0f;
        screenFlash.color = c;
    }

}