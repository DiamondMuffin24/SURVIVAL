using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Door targetDoor; // Drag the door GameObject into this in the Inspector
    public AudioSource interactionSound; // Assign a sound in the Inspector

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (interactionSound != null)
                interactionSound.Play();

            targetDoor.UnlockDoor();
            Debug.Log("Interaction complete.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
