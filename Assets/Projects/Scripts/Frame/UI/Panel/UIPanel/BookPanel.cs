using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using System;

public class BookPanel : BasePanel
{
    public LeapButton BackButton;

    public AutoFlip autoFlip;

    public BookPro _bookPro;

    private Texture2D[] sprites;

    public GameObject PageFront,PageBack;

    public int MaxPage = 176;

    protected override void Awake()
    {
        base.Awake();

        if (Config.Instance)
        {
            MaxPage = Config.Instance.configData.MaxPage;
        }

        for (int i = 0; i < MaxPage; i++)
        {
            if (i / 2 > _bookPro.papers.Count)
            {
                AddPaper(i);
            }
        }
       
    }

    public override void InitFind()
    {
        base.InitFind();
        BackButton = FindTool.FindChildComponent<LeapButton>(transform, "backButton");
        autoFlip = FindTool.FindChildComponent<AutoFlip>(transform, "BookPro");
        _bookPro = FindTool.FindChildComponent<BookPro>(transform, "BookPro");
        
    }

    public override void InitEvent()
    {
        base.InitEvent();
        BackButton.OnStay += BackPanel;
        BackButton.OnEnter += OnEnter;
        BackButton.OnExit += OnExit;
    }

    private void OnEnter(baseOnClick obj)
    {
        obj.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
    }

    private void OnExit(baseOnClick obj)
    {
        obj.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void BackPanel(baseOnClick obj)
    {
        UIState.SwitchPanel(PanelName.DirectoryPanel);
    }

    public override void Open()
    {
        base.Open();
        Init();
    }

    public override void Hide()
    {
        base.Hide();
        Invoke("Reset", 0.5f);
    }

    private void Update()
    {
        if(IsOpen && LeapMotionControl.Instance)
        {
            if(LeapMotionControl.Instance.Hand_Right_IsMoveLeft && sprites!=null)
            {
                if (_bookPro.currentPaper >= sprites.Length / 2)
                    return;

                if(LeapMotionControl.Instance.Hand_Right_IsTracked)
                autoFlip.FlipRightPage();
            }

            if(LeapMotionControl.Instance.Hand_Right_IsMoveRight && sprites != null)
            {
                if (_bookPro.currentPaper <= _bookPro.StartFlippingPaper)
                    return;
                if (LeapMotionControl.Instance.Hand_Right_IsTracked)
                    autoFlip.FlipLeftPage();
            }
        }
    }

    /// <summary>
    /// 设置页面显示
    /// </summary>
    /// <param name="img">当前页面的图片</param>
    private void SetPaper(Texture2D[] img, int currentpage)
    {
        if (currentpage % 2 == 0)
        {
            _bookPro.papers[currentpage / 2].Front.GetComponent<RawImage>().texture = img[currentpage - 1];
        }
        else if (currentpage % 2 == 1)
        {
            _bookPro.papers[currentpage / 2].Back.GetComponent<RawImage>().texture = img[currentpage - 1];
        }
    }

    private void InitPaper(int currentpage)
    {
        if (currentpage % 2 == 0)
        {
            _bookPro.papers[currentpage / 2].Front.GetComponent<RawImage>().texture = null;
        }
        else if (currentpage % 2 == 1)
        {
            _bookPro.papers[currentpage / 2].Back.GetComponent<RawImage>().texture = null;
        }
    }

    /// <summary>
    /// 添加页
    /// </summary>
    private void AddPaper(int pageIndex)
    {
        //创建新的Paper对象
        //创建Paper对象中的前页与后页并设置属性
        Paper paperItem = new Paper();
        GameObject frontPaper = Instantiate(PageFront, _bookPro.transform);
        GameObject backPaper = Instantiate(PageBack, _bookPro.transform);
        frontPaper.name = String.Format("Page{0}", pageIndex);
        backPaper.name = String.Format("Page{0}", pageIndex + 1);

        //添加一个新的页并赋值前页和后页
        _bookPro.papers.Add(paperItem);
        _bookPro.papers[_bookPro.papers.Count - 1].Front = frontPaper;
        _bookPro.papers[_bookPro.papers.Count - 1].Back = backPaper;
        //=====需要手动设置一下可翻页的范围并更新
        UpdateFlipRange();
    }

    /// <summary>
    /// 更新翻页的范围
    /// </summary>
    private void UpdateFlipRange()
    {
        _bookPro.EndFlippingPaper = _bookPro.papers.Count;
        _bookPro.UpdatePages();
    }

    private void Init()
    {
        BackButton.BoxColliderOpen();
        _bookPro.CurrentPaper = 1;
        _bookPro.UpdatePages();

        switch (DirectoryPanel.directoryName)
        {
            case DirectoryName.上善篇:
                if(PageFile.Instance != null && PageFile.Instance.ShangShanP.Count > 0)
                {
                    sprites = PageFile.Instance.ShangShanP.ToArray();
                }
                break;
            case DirectoryName.大爱篇:

                if (PageFile.Instance != null && PageFile.Instance.DaAiP.Count > 0)
                {
                    sprites = PageFile.Instance.DaAiP.ToArray();
                }
                break;
            case DirectoryName.慈怀篇:

                if (PageFile.Instance != null && PageFile.Instance.CiHuaiP.Count > 0)
                {
                    sprites = PageFile.Instance.CiHuaiP.ToArray();
                }
                break;
            case DirectoryName.仁济篇:

                if (PageFile.Instance != null && PageFile.Instance.RenJiP.Count > 0)
                {
                    sprites = PageFile.Instance.RenJiP.ToArray();
                }
                break;
            case DirectoryName.侨捐录:

                if (PageFile.Instance != null && PageFile.Instance.QiaoJuanP.Count > 0)
                {
                    sprites = PageFile.Instance.QiaoJuanP.ToArray();
                }
                break;
            case DirectoryName.无:
                break;
            default:
                break;
        }
        for (int i = 1; i <= sprites.Length; i++)
        {
            SetPaper(sprites, i);
        }
    }





    private void Reset()
    {
        for (int i = 0; i < _bookPro.papers.Count; i++)
        {
            InitPaper(i);
        }
        sprites = null;

        BackButton.BoxColliderHide();

        BackButton.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
