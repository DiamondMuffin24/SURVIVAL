using UnityEngine;
using TMPro;

public class NotePickup : MonoBehaviour
{
    public GameObject noteUI; // Assign the UI panel in the Inspector
    public TextMeshProUGUI noteText; // Assign the text element inside UI
    [TextArea(3, 5)] public string message; // Custom text for each note
    private bool isPlayerNearby = false;

    void Start()
    {
        if (noteUI != null)
            noteUI.SetActive(false); // Hide note UI initially
    }

    void Update()
    {
        // Check if player presses "E" while near the note
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowNote();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void ShowNote()
    {
        if (noteUI != null)
        {
            noteUI.SetActive(true);
            noteText.text = message; // Set custom text
            Time.timeScale = 0f; // Pause game when note is displayed
        }
    }

    public void CloseNote()
    {
        if (noteUI != null)
        {
            noteUI.SetActive(false);
            Time.timeScale = 1f; // Resume game
            Destroy(gameObject); // Remove the note after pickup
        }
    }
}
