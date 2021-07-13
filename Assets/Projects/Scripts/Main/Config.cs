using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigData
{
    /// <summary>
    /// 返回待机页时间
    /// </summary>
    public int Backtime;

    /// <summary>
    /// 最大页码
    /// </summary>
    public int MaxPage;

    /// <summary>
    /// 挥手的灵敏度，目前测试0.42最好
    /// </summary>
    public float deltaVelocity;

    public float MoveVelocity;

    public string Logo;
}


public class Config : MonoBehaviour
{
    public static Config Instance;

    public ConfigData configData  = new ConfigData();

    private string File_name = "config.txt";
    private string Path;

    private void Awake()
    {
        Instance = this;
        configData = new ConfigData();
#if UNITY_STANDALONE_WIN
        Path = Application.streamingAssetsPath + "/" + File_name;
        if (FileHandle.Instance.IsExistFile(Path))
        {
            string st = FileHandle.Instance.FileToString(Path);
            configData = JsonConvert.DeserializeObject<ConfigData>(st);
        }
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        Path = Application.persistentDataPath + "/" + File_name;
        if(FileHandle.Instance.IsExistFile(Path))
        {
            string st = FileHandle.Instance.FileToString(Path);
            configData = JsonConvert.DeserializeObject<ConfigData>(st);
        }
        else
        {
            Path = Application.streamingAssetsPath + "/" + File_name;
            if (FileHandle.Instance.IsExistFile(Path))
            {
                string st = FileHandle.Instance.FileToString(Path);
                configData = JsonConvert.DeserializeObject<ConfigData>(st);
            }
        }
#endif
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void SaveData()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
         Path = Application.persistentDataPath + "/" + File_name;
#endif
        string st = JsonConvert.SerializeObject(configData);
        FileHandle.Instance.SaveFile(st, Path);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
