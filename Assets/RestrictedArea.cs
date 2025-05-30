using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    public string message = "Its blocked by a scanner symbol...";
    public float subtitleDuration = 3f;
    public AudioClip voiceClip; // Assign in Inspector

    private bool playerInArea = false;
    private bool hasSpoken = false;

    private AudioSource audioSource;

    private void Start()
    {
        // You can add an AudioSource to the object, or play from a central one
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void Update()
    {
        if (playerInArea && Input.GetKeyDown(KeyCode.E) && !hasSpoken)
        {
            hasSpoken = true;

            Debug.Log("Player says: " + message);
            SubtitleManager.Instance.ShowSubtitle(message, subtitleDuration);

            if (voiceClip != null)
            {
                audioSource.PlayOneShot(voiceClip);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }
}
