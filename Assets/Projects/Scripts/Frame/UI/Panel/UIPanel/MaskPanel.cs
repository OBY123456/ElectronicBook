using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskPanel : MonoBehaviour
{
    string MaskPath;

    public RawImage Mask;

    private void Awake()
    {
        MaskPath = Application.streamingAssetsPath + "/Mask.png";
        Mask = transform.GetComponent<RawImage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (FileHandle.Instance.IsExistFile(MaskPath))
        {
            Mask.enabled = true;
            Mask.texture = FileHandle.Instance.LoadByIO(MaskPath);
        }
        else
        {
            Mask.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
