using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isUnlocked = false;

    public Sprite unlockedSprite; // Assign the new door sprite in the Inspector
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("Door is now unlocked!");

        if (unlockedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }

        // Optional: disable collider if you want door to disappear completely
        // GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isUnlocked && other.CompareTag("Player"))
        {
            // Let player pass or trigger level change, etc.
            Debug.Log("Player entered the unlocked door area.");
        }
    }
}