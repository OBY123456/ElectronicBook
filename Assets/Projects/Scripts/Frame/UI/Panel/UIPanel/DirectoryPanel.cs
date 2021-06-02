using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using System;

public enum DirectoryName
{
    上善篇,
    大爱篇,
    慈怀篇,
    仁济篇,
    侨捐录,
    无,
}

public class DirectoryPanel : BasePanel
{
    public LeapButton[] leapButtons;

    public static DirectoryName directoryName;

    public override void InitFind()
    {
        base.InitFind();
        leapButtons = FindTool.FindChildNode(transform, "Buttons").GetComponentsInChildren<LeapButton>();
    }

    public override void InitEvent()
    {
        base.InitEvent();
        foreach (var item in leapButtons)
        {
            item.OnStay += SetDirectoryName;
            item.OnEnter += OnEnter;
            item.OnExit += OnExit;
        }
    }

    private void OnEnter(baseOnClick obj)
    {
        obj.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
    }

    private void OnExit(baseOnClick obj)
    {
        obj.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void SetDirectoryName(baseOnClick obj)
    {
        directoryName = (DirectoryName)Enum.Parse(typeof(DirectoryName), obj.name);
        UIState.SwitchPanel(PanelName.BookPanel);
        
    }

    public override void Open()
    {
        base.Open();
        directoryName = DirectoryName.无;
        foreach (var item in leapButtons)
        {
            item.BoxColliderOpen();
        }
    }

    public override void Hide()
    {
        base.Hide();
        foreach (var item in leapButtons)
        {
            item.BoxColliderHide();
            item.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
