﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimBoxSelectionManager : MonoBehaviour
{
    public Camera Camera;
    public RectTransform BoxSelectionPanel;
    public RectTransform UsingWindow;
    public LimTunerManager TunerManager;

    private bool enable = true;
    private Vector2 AnchorPosition;
    private bool ShouldDraw = false;

    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            enable = value;
            if (!value)
            {
                BoxSelectionPanel.sizeDelta = new Vector2();
                ShouldDraw = false;
            }
        }
    }
    public float Size
    {
        get
        {
            return BoxSelectionPanel.sizeDelta.magnitude;
        }
    }

    private void Update()
    {
        if (!TunerManager.isInitialized) return;
        if (!Enable) return;
        DrawBoxSelectionArea();
    }
    private void DrawBoxSelectionArea()
    {
        if (Input.GetMouseButtonUp(0))
        {
            BoxSelectionPanel.sizeDelta = new Vector2();
            ShouldDraw = false;
        }
        Vector2 MousePositionInWindow = LimMousePosition.MousePositionInWindow(UsingWindow);
        if (Input.GetMouseButtonDown(0) && LimMousePosition.IsMouseOverWindow(UsingWindow))
        {
            BoxSelectionPanel.sizeDelta = new Vector2();
            BoxSelectionPanel.anchoredPosition = new Vector2(MousePositionInWindow.x, MousePositionInWindow.y);
            AnchorPosition = BoxSelectionPanel.anchoredPosition;
            ShouldDraw = true;
        }
        if (!ShouldDraw) return;
        if (Input.GetMouseButton(0) && LimMousePosition.IsMouseOverWindow(UsingWindow))
        {
            BoxSelectionPanel.sizeDelta = new Vector2(Mathf.Abs(MousePositionInWindow.x - AnchorPosition.x), Mathf.Abs(AnchorPosition.y - MousePositionInWindow.y));
            BoxSelectionPanel.anchoredPosition = new Vector2(MousePositionInWindow.x > AnchorPosition.x ? AnchorPosition.x : MousePositionInWindow.x,
                AnchorPosition.y > MousePositionInWindow.y ? AnchorPosition.y : MousePositionInWindow.y);
        }
    }
    public Vector2 TunerScreenToWindowPosition(Vector3 ScreenPosition, RectTransform Window)
    {
        return new Vector2(ScreenPosition.x, ScreenPosition.y - Window.sizeDelta.y);
    }
    public bool IsNoteInBoxArea(GameObject Note)
    {
        Vector3 ScreenPosition = Camera.WorldToScreenPoint(Note.transform.position);
        Vector2 WindowPosition = TunerScreenToWindowPosition(ScreenPosition, UsingWindow);
        return LimMathUtil.InRect(WindowPosition, BoxSelectionPanel);
    }
    public bool IsMotionInBoxArea(GameObject Motion)
    {
        Vector3 ScreenPosition = Camera.WorldToScreenPoint(Motion.transform.position);
        Vector2 ScaledPosition = new Vector2(ScreenPosition.x, ScreenPosition.y) * (1920f / Screen.width);
        Vector2 WindowPosition = TunerScreenToWindowPosition(ScaledPosition, UsingWindow);
        return LimMathUtil.InRect(WindowPosition, BoxSelectionPanel);
    }
}