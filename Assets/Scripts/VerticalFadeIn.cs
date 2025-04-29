using UnityEngine;
using UnityEngine.UI;

public class VerticalFadeIn : MonoBehaviour
{
    public RectTransform wipeMask;       // The panel covering the menu
    public float totalDuration = 2f;     // Total duration of the effect
    public int steps = 30;               // Number of vertical steps (more = smoother)

    private Vector2 startPos;
    private Vector2 endPos;
    private float stepHeight;
    private float stepTime;
    private int currentStep = 0;
    private float timer = 0f;

    void Start()
    {
        float menuHeight = ((RectTransform)wipeMask.parent).rect.height;
        stepHeight = menuHeight / steps;
        startPos = new Vector2(wipeMask.anchoredPosition.x, 0);
        endPos = new Vector2(wipeMask.anchoredPosition.x, -menuHeight);
        stepTime = totalDuration / steps;

        wipeMask.anchoredPosition = startPos;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (currentStep < steps && timer >= stepTime)
        {
            timer = 0f;
            currentStep++;

            float newY = -stepHeight * currentStep;
            wipeMask.anchoredPosition = new Vector2(startPos.x, newY);
        }
    }
}
