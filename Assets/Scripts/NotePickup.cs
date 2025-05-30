using UnityEngine;
using TMPro;

public class NotePickup : MonoBehaviour
{
    public GameObject noteUI; // Assign the UI panel in the Inspector
    public TextMeshProUGUI noteText; // Assign the text element inside UI
    [TextArea(3, 5)] public string message; // Custom text for each note

    private bool isPlayerNearby = false;
    private bool isNoteOpen = false;

    void Start()
    {
        if (noteUI != null)
            noteUI.SetActive(false); // Hide note UI initially
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isNoteOpen)
            {
                CloseNote();
            }
            else
            {
                ShowNote();
            }
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
            if (isNoteOpen)
            {
                CloseNote(); // Optional: auto-close when player walks away
            }
        }
    }

    void ShowNote()
    {
        if (noteUI != null)
        {
            noteUI.SetActive(true);
            noteText.text = message;
            Time.timeScale = 0f;
            isNoteOpen = true;
        }
    }

    void CloseNote()
    {
        if (noteUI != null)
        {
            noteUI.SetActive(false);
            Time.timeScale = 1f;
            isNoteOpen = false;
            Destroy(gameObject); // Remove the note after reading
        }
    }
}
