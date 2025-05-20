using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public HandStuckManager handStuckManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handStuckManager.TriggerHandStuck();
            // Optional: disable the collider so it doesn’t retrigger
            GetComponent<Collider>().enabled = false;
        }
    }
}
