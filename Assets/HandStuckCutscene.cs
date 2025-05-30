using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandStuckCutscene : MonoBehaviour
{
    public int nextSceneIndex = -1;
    public string nextSceneName;
    [Header("References")]
    public Animator handAnimator;
    public GameObject player;

    [Header("UI")]
    public Slider mashSlider;          // UI Slider for mash progress
    public Text timerTextUI;           // Optional timer text
    public FailEffects failEffects;    // Optional reference to fail effects

    [Header("Cutscene Settings")]
    public float struggleDuration = 5f;
    public int requiredPresses = 20;
    public KeyCode mashKey = KeyCode.E;

    [Header("Decay Settings")]
    public float decayRate = 3f;       // How fast mash progress decays (presses per second)
    public float decayDelay = 1f;      // How long after last press before decay starts (in seconds)

    private bool isCutsceneActive = false;
    private float currentPresses = 0;
    private float lastPressTime = 0f;

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        isCutsceneActive = true;
        currentPresses = 0;
        float timer = 0f;
        lastPressTime = -decayDelay; // So it doesn't start decaying immediately

        // Start stuck animation
        handAnimator.Play("HandStuck");

        // Prepare UI
        if (mashSlider != null)
        {
            mashSlider.minValue = 0;
            mashSlider.maxValue = requiredPresses;
            mashSlider.value = 0;
        }

        yield return new WaitForSeconds(0.5f);

        // Mashing phase
        while (timer < struggleDuration && currentPresses < requiredPresses)
        {
            if (Input.GetKeyDown(mashKey))
            {
                currentPresses++;
                currentPresses = Mathf.Min(currentPresses, requiredPresses);
                lastPressTime = timer;
                if (currentPresses >= requiredPresses)

                    handAnimator.Play("PullingHand", -1, 0f);
            }
            else if (timer - lastPressTime >= decayDelay)
            {
                currentPresses -= decayRate * Time.deltaTime;
                currentPresses = Mathf.Max(0f, currentPresses);
            }

            // Update slider
            if (mashSlider != null)
                mashSlider.value = currentPresses;

            timer += Time.deltaTime;
            yield return null;
        }

        // Outcome
        if (currentPresses >= requiredPresses)
        {
            handAnimator.Play("HandFree");
            Debug.Log("Player escaped!");

            yield return new WaitForSeconds(1f); // Let the animation finish

            // Load next scene
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (nextSceneIndex >= 0)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }

            yield break;
        }

    }
}
