using UnityEngine;
using UnityEngine.UI;

public class HandStuckManager : MonoBehaviour
{
    public Animator handAnimator; // Reference to your animator
    public int requiredMashCount = 20;
    public float mashTimeLimit = 5f; // Optional timer
    public Text mashCounterText; // UI to show progress (optional)

    private int currentMashCount;
    private bool isStuck = false;
    private float timer;


    void Update()
    {
        if (isStuck)
        {
            timer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentMashCount++;
                if (mashCounterText != null)
                    mashCounterText.text = $"{currentMashCount} / {requiredMashCount}";
            }

            if (currentMashCount >= requiredMashCount)
            {
                ReleaseHand();

            }

            if (timer <= 0)
            {
                // Optional: handle failure condition
                Debug.Log("Failed to escape!");
                isStuck = false;
            }
        }
    }

    public void TriggerHandStuck()
    {

        isStuck = true;
        timer = mashTimeLimit;
        currentMashCount = 0;

        handAnimator.SetTrigger("Stuck"); // Ensure you have a "Stuck" trigger in Animator
    }

    private void ReleaseHand()
    {
        isStuck = false;
        handAnimator.SetTrigger("Released"); // Make sure this is set up in Animator
        Debug.Log("Hand released!");
    }
}
