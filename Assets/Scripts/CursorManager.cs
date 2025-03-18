using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor; // Assign the object texture here
    private Vector2 cursorHotspot; // Adjusts the click point

    void Start()
    {
        cursorHotspot = new Vector2(customCursor.width / 2, customCursor.height / 2); // Center the cursor
    }

    public void ChangeCursor()
    {
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Resets to default
    }
}
