using UnityEngine;
using UnityEngine.UI;

public class CustomCursorUI : MonoBehaviour
{
    public RectTransform crosshairUI; // Reference to the UI crosshair (Image)

    void Awake()
    {
        // Hide the system cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // Lock the cursor within the game window
    }

    void Update()
    {
        // Convert the mouse position to screen coordinates for the UI crosshair
        Vector2 mousePos = Input.mousePosition;
        crosshairUI.position = mousePos;

        // Keep the system cursor invisible
        Cursor.visible = false;
    }
}
