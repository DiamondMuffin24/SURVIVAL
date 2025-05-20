using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public HandStuckManager handStuckManager;
    public float interactionRange = 2f;
    public Transform player;

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            handStuckManager.TriggerHandStuck();
        }
    }
}