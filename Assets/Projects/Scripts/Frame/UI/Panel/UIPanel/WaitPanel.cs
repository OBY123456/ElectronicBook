using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using MTFrame.MTEvent;
using System;
using Leap.Unity;
using DG.Tweening;

public class WaitPanel : BasePanel
{
    private float BackTime;
    private float Back_Time;
    private bool IsBack;

    private HandModelBase rightHandModel;

    protected override void Start()
    {
        base.Start();

        if(LeapMotionControl.Instance)
        {
            rightHandModel = LeapMotionControl.Instance.rightHandModel;
        }
        

        if (Config.Instance)
        BackTime = Config.Instance.configData.Backtime;
    }

    private void Update()
    {
        if(LeapMotionControl.Instance && rightHandModel.IsTracked)
        {
            IsBack = false;
            Back_Time = BackTime;
            if(IsOpen)
            {
                UIState.SwitchPanel(PanelName.DirectoryPanel);
            }
            
        }
        else
        {
            IsBack = true;
            if (Back_Time > 0 && IsBack)
            {
                Back_Time -= Time.deltaTime;

                if (Back_Time <= 0)
                {
                    IsBack = false;
                    Back_Time = BackTime;
                    UIState.SwitchPanel(PanelName.WaitPanel);
                }
            }
        }
    }
}
