using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using MTFrame.MTEvent;
using System;
using RenderHeads.Media.AVProVideo;
using Leap.Unity;
using DG.Tweening;

public class WaitPanel : BasePanel
{
    //public MediaPlayer mediaPlayer;

    private float BackTime;
    private float Back_Time;
    private bool IsBack;

    private HandModelBase rightHandModel;

    protected override void Start()
    {
        base.Start();
        //Reset();

        if(LeapMotionControl.Instance)
        {
            rightHandModel = LeapMotionControl.Instance.rightHandModel;
        }
        

        if (Config.Instance)
        BackTime = Config.Instance.configData.Backtime;
    }

    public override void InitFind()
    {
        base.InitFind();
       // mediaPlayer = FindTool.FindChildComponent<MediaPlayer>(transform, "Video");
    }

    public override void InitEvent()
    {
        base.InitEvent();
        //mediaPlayer.Events.AddListener(Complete);
    }

    private void Complete(MediaPlayer arg0, MediaPlayerEvent.EventType arg1, ErrorCode arg2)
    {
        switch (arg1)
        {
            case MediaPlayerEvent.EventType.FinishedPlaying:
                arg0.Stop();
                UIState.SwitchPanel(PanelName.DirectoryPanel);
                break;
            default:
                break;
        }
    }

    public override void Open()
    {
        base.Open();
        //IsPlay = false;
    }

    public override void Hide()
    {
        base.Hide();
        //Reset();
    }

    //private bool IsPlay;
    //private void VideoPlay()
    //{
    //    if(!IsPlay)
    //    {
    //        IsPlay = true;
    //        mediaPlayer.gameObject.GetComponent<CanvasGroup>().DOFade(1,0.5f);
    //        mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "AVProVideoSamples/BigBuckBunny_720p30.mp4");
    //    }
    //}

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

    //private void Reset()
    //{
    //    mediaPlayer.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    //}
}
