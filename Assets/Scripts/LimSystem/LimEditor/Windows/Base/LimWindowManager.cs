﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LimWindowManager : MonoBehaviour
{
    public bool Moveable, ResizeableVertical, ResizeableHorizontal;
    public LimEditorManager EditorManager;
    public RectTransform WindowRectTransform;
    public Text NameUiText;
    public GameObject ResizeHandles;
    public string WindowName
    {
        get { return NameUiText.text; }
        set { NameUiText.text = value; }
    }

    private bool isHandlePressed = false, isLeftResizePressed = false, isBottomResizePressed = false, isRightResizePressed = false, isTopResizePressed = false;
    private Vector3 LastMousePosition;

    private void Update()
    {
        Scan();
    }
    private void OnEnable()
    {
        transform.SetSiblingIndex(transform.parent.childCount - 2);
    }

    public void Scan()
    {
        MoveWindow();
        ResizeWindow();
        LastMousePosition = LimMousePosition.MousePosition;
        float Height = 1920 * Screen.height / Screen.width;
        if (WindowRectTransform.anchoredPosition.x < 0) WindowRectTransform.anchoredPosition = new Vector2(0, WindowRectTransform.anchoredPosition.y);
        if (WindowRectTransform.anchoredPosition.x > 1760) WindowRectTransform.anchoredPosition = new Vector2(1760, WindowRectTransform.anchoredPosition.y);
        if (WindowRectTransform.anchoredPosition.y > -60) WindowRectTransform.anchoredPosition = new Vector2(WindowRectTransform.anchoredPosition.x, -60);
        if (WindowRectTransform.anchoredPosition.y < -Height) WindowRectTransform.anchoredPosition = new Vector2(WindowRectTransform.anchoredPosition.x, -Height);
    }
    public void MoveWindow()
    {
        if (!Moveable) return;
        if (!isHandlePressed) return;
        if (LastMousePosition.x > 1920 || LastMousePosition.y > 1080) return;
        Vector2 Delta = LimMousePosition.MousePosition - LastMousePosition;
        WindowRectTransform.anchoredPosition += Delta;
    }
    public void ResizeWindow()
    {
        if (LastMousePosition.x > 1920 || LastMousePosition.y > 1080) return;
        ResizeLeft();
        ResizeBottom();
        ResizeRight();
        ResizeTop();
        if (WindowRectTransform.sizeDelta.x <= 0) WindowRectTransform.sizeDelta = new Vector2(1, WindowRectTransform.sizeDelta.y);
        if (WindowRectTransform.sizeDelta.y <= 0) WindowRectTransform.sizeDelta = new Vector2(WindowRectTransform.sizeDelta.x, 1);
    }
    private void ResizeLeft()
    {
        if (!ResizeableHorizontal) return;
        if (!isLeftResizePressed) return;
        Vector2 Delta = LimMousePosition.MousePosition - LastMousePosition;
        Delta.y = 0;
        WindowRectTransform.anchoredPosition += Delta;
        WindowRectTransform.sizeDelta -= Delta;
    }
    private void ResizeBottom()
    {
        if (!ResizeableVertical) return;
        if (!isBottomResizePressed) return;
        Vector2 Delta = LimMousePosition.MousePosition - LastMousePosition;
        Delta.x = 0;
        WindowRectTransform.sizeDelta -= Delta;
    }
    private void ResizeRight()
    {
        if (!ResizeableHorizontal) return;
        if (!isRightResizePressed) return;
        Vector2 Delta = LimMousePosition.MousePosition - LastMousePosition;
        Delta.y = 0;
        WindowRectTransform.sizeDelta += Delta;
    }
    private void ResizeTop()
    {
        if (!ResizeableVertical) return;
        if (!isTopResizePressed) return;
        Vector2 Delta = LimMousePosition.MousePosition - LastMousePosition;
        Delta.x = 0;
        WindowRectTransform.sizeDelta += Delta;
        WindowRectTransform.anchoredPosition += Delta;
    }

    public void OnMovePointerDown()
    {
        isHandlePressed = true;
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }
    public void OnMovePointerUp()
    {
        isHandlePressed = false;
    }
    public void OnResizeLeftPointerDown()
    {
        isLeftResizePressed = true;
    }
    public void OnResizeLeftPointerUp()
    {
        isLeftResizePressed = false;
    }
    public void OnResizeBottomPointerDown()
    {
        isBottomResizePressed = true;
    }
    public void OnResizeBottomPointerUp()
    {
        isBottomResizePressed = false;
    }
    public void OnResizeRightPointerDown()
    {
        isRightResizePressed = true;
    }
    public void OnResizeRightPointerUp()
    {
        isRightResizePressed = false;
    }
    public void OnResizeTopPointerDown()
    {
        isTopResizePressed = true;
    }
    public void OnResizeTopPointerUp()
    {
        isTopResizePressed = false;
    }

    private Coroutine ResizeHandleAnim;
    public void OnPointerEnterResizeHandle()
    {
        if (ResizeHandleAnim != null) StopCoroutine(ResizeHandleAnim);
        ResizeHandleAnim = StartCoroutine(ShowResizeHandleCoroutine());
    }
    IEnumerator ShowResizeHandleCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        foreach (Image Img in ResizeHandles.transform.GetComponentsInChildren<Image>())
        {
            Img.color = new Color(1, 1, 1, 0.7f);
        }
    }
    public void OnPointerExitResizeHandle()
    {
        if (ResizeHandleAnim != null) StopCoroutine(ResizeHandleAnim);
        foreach (Image Img in ResizeHandles.transform.GetComponentsInChildren<Image>())
        {
            Img.color = new Color(0, 0, 0, 0);
        }
    }
}