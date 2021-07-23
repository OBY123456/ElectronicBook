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
    特别篇,
    无,
}

public class DirectoryPanel : BasePanel
{
    public LeapButton[] leapButtons;
    public LeapButton SpecialBtn;

    public static DirectoryName directoryName;

    protected override void Start()
    {
        base.Start();
        if(Config.Instance)
        {
            if(Config.Instance.configData.是否开启特别篇)
            {
                SpecialBtn.gameObject.SetActive(true);
            }
            else
            {
                SpecialBtn.gameObject.SetActive(false);
            }
        }
    }

    public override void InitFind()
    {
        base.InitFind();
        leapButtons = FindTool.FindChildNode(transform, "Buttons").GetComponentsInChildren<LeapButton>();
        SpecialBtn = FindTool.FindChildComponent<LeapButton>(transform, "5");
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

        SpecialBtn.OnStay += SetDirectoryName;
        SpecialBtn.OnEnter += OnEnter;
        SpecialBtn.OnExit += OnExit;
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

        if(SpecialBtn.gameObject.activeSelf == true)
        {
            SpecialBtn.BoxColliderOpen();
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

        if (SpecialBtn.gameObject.activeSelf == true)
        {
            SpecialBtn.BoxColliderHide();
        }
    }
}
