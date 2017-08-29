using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour {

    [SerializeField] private MouseCursor mouseCursorIdle = null;
    [SerializeField] private MouseCursor mouseCursorRed = null;
    [SerializeField] private MouseCursor mouseCursorTeleport = null;
    [SerializeField] private MouseCursor mouseCursorInvisable = null;
    [SerializeField] private MouseCursor mouseCursorGreen = null;

    private int ScreenWidthRef = 2560;
    private int ScreenHightRef = 1600;

    public void SetIdleCursor()
    {
        Cursor.SetCursor(mouseCursorIdle.cursorTexture,mouseCursorIdle.hotSpot, mouseCursorIdle.cursorMode);
    }

    public void SetInvisibleCursor()
    {
        Cursor.SetCursor(mouseCursorInvisable.cursorTexture, mouseCursorInvisable.hotSpot, mouseCursorInvisable.cursorMode);
    }

    public void SetRedCursor()
    {
        Cursor.SetCursor(mouseCursorRed.cursorTexture, mouseCursorRed.hotSpot, mouseCursorRed.cursorMode);
    }

    public void SetTeleportCursor()
    {
        Cursor.SetCursor(mouseCursorTeleport.cursorTexture, mouseCursorTeleport.hotSpot, mouseCursorTeleport.cursorMode);
    }

    public void SetGreenCursor()
    {
        Cursor.SetCursor(mouseCursorGreen.cursorTexture, mouseCursorGreen.hotSpot, mouseCursorGreen.cursorMode);
    }

    private void Start()
    {
        ResizeCursor(ref mouseCursorIdle.cursorTexture, ref mouseCursorIdle.hotSpot);
        ResizeCursor(ref mouseCursorInvisable.cursorTexture, ref mouseCursorInvisable.hotSpot);
        ResizeCursor(ref mouseCursorRed.cursorTexture, ref mouseCursorRed.hotSpot);
        ResizeCursor(ref mouseCursorTeleport.cursorTexture, ref mouseCursorTeleport.hotSpot);
        ResizeCursor(ref mouseCursorGreen.cursorTexture, ref mouseCursorGreen.hotSpot);

        SetIdleCursor();
    }

    private void ResizeCursor(ref Texture2D texture, ref Vector2 hotSpot)
    {
        print("antes: " + texture.width.ToString());
        texture.Resize(texture.width * (Screen.width / ScreenWidthRef),texture.height * (Screen.height / ScreenHightRef));
        print("depois: " + texture.width.ToString());
        hotSpot = new Vector2(hotSpot.x * (Screen.width / ScreenWidthRef), hotSpot.y * (Screen.height / ScreenHightRef));
    }
}
