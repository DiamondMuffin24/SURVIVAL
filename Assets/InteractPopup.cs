using UnityEngine;

public class InteractPopup : MonoBehaviour
{
    public GameObject interactText; // Assign the popup UI in inspector
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted!");
            // Put interaction logic here
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            playerInRange = false;
        }
    }
}
