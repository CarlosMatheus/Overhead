using UnityEngine;

[System.Serializable]
public class MouseCursor
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
}
