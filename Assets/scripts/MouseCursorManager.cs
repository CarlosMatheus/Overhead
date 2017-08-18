using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour {

    [SerializeField] private MouseCursor mouseCursorIdle;
    [SerializeField] private MouseCursor mouseCursorRed;
    [SerializeField] private MouseCursor mouseCursorTeleport;

    public void SetIdleCursor()
    {
        Cursor.SetCursor(mouseCursorIdle.cursorTexture,mouseCursorIdle.hotSpot, mouseCursorIdle.cursorMode);
    }
    public void SetInvisibleCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void SetRedCursor()
    {
        Cursor.SetCursor(mouseCursorRed.cursorTexture, mouseCursorRed.hotSpot, mouseCursorRed.cursorMode);
    }
    public void SetTeleportCursor()
    {
        Cursor.SetCursor(mouseCursorTeleport.cursorTexture, mouseCursorTeleport.hotSpot, mouseCursorTeleport.cursorMode);
    }

    private void Start()
    {
        SetIdleCursor();
    }

}
