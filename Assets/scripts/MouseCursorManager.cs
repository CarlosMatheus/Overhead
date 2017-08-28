﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour {

    [SerializeField] private MouseCursor mouseCursorIdle = null;
    [SerializeField] private MouseCursor mouseCursorRed = null;
    [SerializeField] private MouseCursor mouseCursorTeleport = null;
    [SerializeField] private MouseCursor mouseCursorInvisable = null;

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

    private void Start()
    {
        SetIdleCursor();
    }
}
