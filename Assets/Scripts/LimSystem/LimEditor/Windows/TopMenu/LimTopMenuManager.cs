﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class LimTopMenuManager : MonoBehaviour
{
    public LimProjectManager ProjectManager;
    public GameObject FilePanel, WindowPanel, SettingPanel, PluginPanel;
    public Text FileText;
    public Text NewProjectText, OpenProjectText, SaveProjectText, SaveAsProjectText, ExitText;
    public Text WindowText;
    public Text InspectorText, TunerWindowText, TimeLineText, MusicPlayerText, CreatorText;
    public Text SettingText;
    public Text PreferencesText;
    public Text PluginText;
    public Text PluginCenterText;
    public Text TopText;

    private void Start()
    {
        if (LimSystem.ChartContainer == null) return;
        TopText.text = string.Format("Lanotalium - <{0}>", LimSystem.ChartContainer.ChartProperty.ChartName);
    }
    public void SetTexts()
    {
        FileText.text = LimLanguageManager.TextDict["TopMenu_File"];
        NewProjectText.text = LimLanguageManager.TextDict["TopMenu_File_NewProject"];
        OpenProjectText.text = LimLanguageManager.TextDict["TopMenu_File_OpenProject"];
        SaveProjectText.text = LimLanguageManager.TextDict["TopMenu_File_SaveProject"];
        SaveAsProjectText.text = LimLanguageManager.TextDict["TopMenu_File_SaveAsProject"];
        ExitText.text = LimLanguageManager.TextDict["TopMenu_File_Exit"];
        WindowText.text = LimLanguageManager.TextDict["TopMenu_Window"];
        InspectorText.text = LimLanguageManager.TextDict["TopMenu_Window_Inspector"];
        TunerWindowText.text = LimLanguageManager.TextDict["TopMenu_Window_Tuner"];
        TimeLineText.text = LimLanguageManager.TextDict["TopMenu_Window_TimeLine"];
        MusicPlayerText.text = LimLanguageManager.TextDict["TopMenu_Window_MusicPlayer"];
        CreatorText.text = LimLanguageManager.TextDict["TopMenu_Window_Creator"];
        SettingText.text = LimLanguageManager.TextDict["TopMenu_Setting"];
        PreferencesText.text = LimLanguageManager.TextDict["TopMenu_Setting_Preferences"];
        PluginText.text = LimLanguageManager.TextDict["TopMenu_Plugin"];
        PluginCenterText.text = LimLanguageManager.TextDict["TopMenu_Plugin_PluginCenter"];
    }
    public void OpenFileMenu()
    {
        if (FilePanel.activeInHierarchy) FilePanel.SetActive(false);
        else FilePanel.SetActive(true);
    }
    public void NewProject()
    {
        ProjectManager.CreateProject();
    }
    public void LoadProject()
    {
        ProjectManager.LoadProject();
    }
    public void SaveProject()
    {
        if (LimSystem.ChartContainer == null) return;
        ProjectManager.SaveProject();
    }
    public void SaveAsProject()
    {
        if (LimSystem.ChartContainer == null) return;
        ProjectManager.SaveAsProject();
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void OpenWindowMenu()
    {
        if (WindowPanel.activeInHierarchy) WindowPanel.SetActive(false);
        else WindowPanel.SetActive(true);
    }
    public void OpenSettingMenu()
    {
        if (SettingPanel.activeInHierarchy) SettingPanel.SetActive(false);
        else SettingPanel.SetActive(true);
    }
    public void OpenPluginMenu()
    {
        if (PluginPanel.activeInHierarchy) PluginPanel.SetActive(false);
        else PluginPanel.SetActive(true);
    }
}