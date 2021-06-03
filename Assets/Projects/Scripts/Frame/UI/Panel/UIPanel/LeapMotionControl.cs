using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class LeapMotionControl : MonoBehaviour
{
    public static LeapMotionControl Instance;

    [HideInInspector]
    public LeapProvider provider;
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;

    const float smallestVelocity = 0.1f;
    /// <summary>
    /// 挥手的幅度，经测试这个幅度最好
    /// </summary>
    public float deltaVelocity = 0.42f;
    const float deltaCloseFinger = 0.06f;

    /// <summary>
    /// 右手，是否向左挥手
    /// </summary>
    [HideInInspector]
    public bool Hand_Right_IsMoveLeft;

    /// <summary>
    /// 右手，是否向右挥手
    /// </summary>
    [HideInInspector]
    public bool Hand_Right_IsMoveRight;

    /// <summary>
    /// 右手，是否握拳
    /// </summary>
    [HideInInspector]
    public bool Hand_Right_IsCloseHand;

    /// <summary>
    /// 右手，是否检测到
    /// </summary>
    [HideInInspector]
    public bool Hand_Right_IsTracked;

    private void Awake()
    {
        Instance = this;
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Config.Instance)
        {
            deltaVelocity = Config.Instance.configData.deltaVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rightHandModel.IsTracked)
        {
            RightHandMove();
            RightHandCloseHand();
            Hand_Right_IsTracked = true;
        }
        else
        {
            Hand_Right_IsTracked = false;
        }
    }

    /// <summary>
    /// 右手移动
    /// </summary>
    private void RightHandMove()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                if (IsMoveLeft(hand))
                {
                    LogMsg.Instance.Log("右手向左挥手");
                    Hand_Right_IsMoveLeft = true;
                }
                else
                {
                    Hand_Right_IsMoveLeft = false;
                }

                if (IsMoveRight(hand))
                {
                    LogMsg.Instance.Log("右手向右挥手");
                    Hand_Right_IsMoveRight = true;
                }
                else
                {
                    Hand_Right_IsMoveRight = false;
                }
            }
        }
    }

    private void RightHandCloseHand()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                if(IsCloseHand(hand))
                {
                    LogMsg.Instance.Log("右手握拳");
                    Hand_Right_IsCloseHand = true;
                }
                else
                {
                    LogMsg.Instance.Log("右手没握拳");
                    Hand_Right_IsCloseHand = false;
                }
            }
        }
    }

    /// <summary>
    /// 手划向右边
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsMoveRight(Hand hand)
    {
        //Debug.Log(hand.PalmVelocity);
        return hand.PalmVelocity.x > deltaVelocity && !IsStationary(hand);
    }

    /// <summary>
    /// 手划向左边
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsMoveLeft(Hand hand)
    {
        //Debug.Log(hand.PalmVelocity);
        //x轴移动的速度   deltaVelocity = 0.7f    isStationary (hand)  判断hand是否禁止
        return hand.PalmVelocity.x < -deltaVelocity && !IsStationary(hand);
    }

    /// <summary>
    /// 手向上 
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsMoveUp(Hand hand) 
    {
        return hand.PalmVelocity.y > deltaVelocity && !IsStationary(hand);
    }

    /// <summary>
    /// 手向下
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsMoveDown(Hand hand) 
    {
        return hand.PalmVelocity.y < -deltaVelocity && !IsStationary(hand);
    }

    /// <summary>
    /// 固定不动的
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsStationary(Hand hand)
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;
    }

    /// <summary>
    /// 是否握拳
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsCloseHand(Hand hand)     
    {
        List<Finger> listOfFingers = hand.Fingers;
        int count = 0;
        for (int f = 0; f < listOfFingers.Count; f++)
        { //循环遍历所有的手~~
            Finger finger = listOfFingers[f];
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)    // Magnitude  向量的长度 。是(x*x+y*y+z*z)的平方根。    //float  deltaCloseFinger = 0.05f;
            {
                count++;
                //  if (finger.Type == Finger.FingerType.TYPE_THUMB)
                //  Debug.Log ((finger.TipPosition - hand.PalmPosition).Magnitude);
            }
        }
        return (count == 5);
    }

    /// <summary>
    /// 手掌全展开~
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool IsOpenFullHand(Hand hand)
    {
        //Debug.Log (hand.GrabStrength + " " + hand.PalmVelocity + " " +  hand.PalmVelocity.Magnitude);
        //return hand.GrabStrength == 0;这个经过测试不行
        if (hand.Fingers[0].IsExtended && hand.Fingers[1].IsExtended
            && hand.Fingers[2].IsExtended && hand.Fingers[3].IsExtended
            && hand.Fingers[4].IsExtended)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判断手指是否伸展或弯曲,下面是食指伸直,其它手指弯曲
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="arr"></param>
    /// <returns></returns>
    public bool JudgeIndexDetector(Hand hand)
    {
        if (!hand.Fingers[0].IsExtended && hand.Fingers[1].IsExtended && !hand.Fingers[2].IsExtended && !hand.Fingers[3].IsExtended
            && !hand.Fingers[4].IsExtended)
        {
            return true;
        }
        return false;
    }
}
