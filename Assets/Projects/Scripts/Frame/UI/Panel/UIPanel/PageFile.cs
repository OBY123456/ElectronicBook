using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFile : MonoBehaviour
{
    public static PageFile Instance;

    /// <summary>
    /// 上善篇
    /// </summary>
    public List<Texture2D> ShangShanP = new List<Texture2D>();
    private List<string> ShangShanImagePath = new List<string>();
    private string ShangShanPath = "/电子书切图/上善篇";

    /// <summary>
    /// 大爱篇
    /// </summary>
    public List<Texture2D> DaAiP = new List<Texture2D>();
    private List<string> DaAiImagePath = new List<string>();
    private string DaAiPath = "/电子书切图/大爱篇";

    /// <summary>
    /// 慈怀篇
    /// </summary>
    public List<Texture2D> CiHuaiP = new List<Texture2D>();
    private List<string> CiHuaiImagePath = new List<string>();
    private string CiHuaiPath = "/电子书切图/慈怀篇";

    /// <summary>
    /// 仁济篇
    /// </summary>
    public List<Texture2D> RenJiP = new List<Texture2D>();
    private List<string> RenJiImagePath = new List<string>();
    private string RenJiPath = "/电子书切图/仁济篇";

    /// <summary>
    /// 桥捐篇
    /// </summary>
    public List<Texture2D> QiaoJuanP = new List<Texture2D>();
    private List<string> QiaoJuanImagePath = new List<string>();
    private string QiaoJuanPath = "/电子书切图/侨捐录";

    /// <summary>
    /// 特别篇
    /// </summary>
    public List<Texture2D> TeBieP = new List<Texture2D>();
    private List<string> TeBieImagePath = new List<string>();
    private string TeBiePath = "/电子书切图/特别篇";

    private void Awake()
    {
        Instance = this;

        InitBook(DirectoryName.上善篇);
        InitBook(DirectoryName.大爱篇);
        InitBook(DirectoryName.慈怀篇);
        InitBook(DirectoryName.仁济篇);
        InitBook(DirectoryName.侨捐录);   
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Config.Instance)
        {
            if(Config.Instance.configData.是否开启特别篇)
            {
                InitBook(DirectoryName.特别篇);
            }
        }
    }

    private void InitBook(DirectoryName name)
    {
        switch (name)
        {
            case DirectoryName.上善篇:
                if (ShangShanImagePath == null || ShangShanImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.上善篇);

                }
                for (int i = 0; i < ShangShanImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(ShangShanImagePath[i]);
                    ShangShanP.Add(sprite);
                }
                break;
            case DirectoryName.大爱篇:
                if (DaAiImagePath == null || DaAiImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.大爱篇);

                }
                for (int i = 0; i < DaAiImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(DaAiImagePath[i]);
                    DaAiP.Add(sprite);
                }
                break;
            case DirectoryName.慈怀篇:
                if (CiHuaiImagePath == null || CiHuaiImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.慈怀篇);

                }
                for (int i = 0; i < CiHuaiImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(CiHuaiImagePath[i]);
                    CiHuaiP.Add(sprite);
                }
                break;
            case DirectoryName.仁济篇:
                if (RenJiImagePath == null || RenJiImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.仁济篇);

                }
                for (int i = 0; i < RenJiImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(RenJiImagePath[i]);
                    RenJiP.Add(sprite);
                }
                break;
            case DirectoryName.侨捐录:
                if (QiaoJuanImagePath == null || QiaoJuanImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.侨捐录);

                }
                for (int i = 0; i < QiaoJuanImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(QiaoJuanImagePath[i]);
                    QiaoJuanP.Add(sprite);
                }
                break;
            case DirectoryName.特别篇:
                if (TeBieImagePath == null || TeBieImagePath.Count <= 0)
                {
                    InitBookList(DirectoryName.特别篇);

                }
                for (int i = 0; i < TeBieImagePath.Count; i++)
                {
                    Texture2D sprite = FileHandle.Instance.LoadByIO(TeBieImagePath[i]);
                    TeBieP.Add(sprite);
                }
                break;
            case DirectoryName.无:
                break;
            default:
                break;
        }
    }

    private void InitBookList(DirectoryName name)
    {
        switch (name)
        {
            case DirectoryName.上善篇:
                ShangShanImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + ShangShanPath);
                break;
            case DirectoryName.大爱篇:
                DaAiImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + DaAiPath);
                break;
            case DirectoryName.慈怀篇:
                CiHuaiImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + CiHuaiPath);
                break;
            case DirectoryName.仁济篇:
                RenJiImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + RenJiPath);
                break;
            case DirectoryName.侨捐录:
                QiaoJuanImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + QiaoJuanPath);
                break;
            case DirectoryName.特别篇:
                TeBieImagePath = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + TeBiePath);
                break;
            case DirectoryName.无:
                break;
            default:
                break;
        }
    }
}
