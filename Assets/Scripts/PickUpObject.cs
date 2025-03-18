using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool isPickedUp = false;
    private CursorManager cursorManager;
    public Texture2D objectCursor; // Assign a cursor image

    void Start()
    {
        cursorManager = FindFirstObjectByType<CursorManager>();
    }

    void OnMouseDown()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            cursorManager.customCursor = objectCursor;
            cursorManager.ChangeCursor();
        }
    }
}
