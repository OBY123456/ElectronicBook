using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Leap.Unity;
using Leap;
using Image = UnityEngine.UI.Image;

public class HandFllow : MonoBehaviour
{
    public float Times = 2.0f;
    public bool IsComplete;

    private LeapProvider provider;
    private HandModelBase leftHandModel;
    private HandModelBase rightHandModel;

    Tweener tweener;

    private Image image,hand;

    public Sprite[] Hands;

    // Start is called before the first frame update
    void Start()
    {
        provider = LeapMotionControl.Instance.provider;
        leftHandModel = LeapMotionControl.Instance.leftHandModel;
        rightHandModel = LeapMotionControl.Instance.rightHandModel;
        image = transform.GetChild(0).GetComponent<Image>();
        hand = transform.GetComponent<Image>();

        if(Config.Instance)
        {
            num = Config.Instance.configData.MoveVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandModel.IsTracked)
        {
            FllowHandImage();
            //if(LeapMotionControl.Instance.Hand_Right_IsCloseHand)
            //{
            //    if(hand.sprite.name.Contains("手掌"))
            //    {
            //        hand.sprite = Hands[1];
            //    }
            //}
            //else
            //{
            //    if (hand.sprite.name.Contains("握拳"))
            //    {
            //        hand.sprite = Hands[0];
            //    }
            //}
        }
        else
        {
            transform.position = Vector3.one * 10000;
            if(image.fillAmount == 1)
            {
                image.fillAmount = 0;
            }
        }
    }

    private Vector3 vector;
    float num = 1;

    private void FllowHandImage()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                //手的刚体一定要锁住Z轴，不然会出Bug，手没触碰到按钮的触发器也能触发
                vector = Camera.main.WorldToScreenPoint(new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z));
                transform.position = vector * num;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<baseOnClick>())
        {
            tweener.Kill();
            //transform.DOKill();
            collision.GetComponent<baseOnClick>().TriggerEnter();
            image.fillAmount = 1;
            IsComplete = false;
            //Debug.Log(collision.gameObject.name);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<baseOnClick>())
        {
            
            if(!IsComplete)
            {
                tweener = image.DOFillAmount(0, Times).OnComplete(() => {
                                  collision.GetComponent<baseOnClick>().TriggerStay();
                                  //IsComplete = true;
                              }).SetEase(Ease.Linear);
                IsComplete = true;
            }

            //if (LeapMotionControl.Instance.Hand_Right_IsCloseHand)
            //{
            //    if (!tweener.IsPlaying())
            //    {
            //        tweener.Play();
            //    }

            //}
            //else
            //{
            //    if (tweener.IsPlaying())
            //    {
            //        tweener.Pause();
            //    }
            //}
            //tweener.OnComplete(() =>
            //{
            //    //LogMsg.Instance.Log("Hand_Right_IsCloseHand==" + LeapMotionControl.Instance.Hand_Right_IsCloseHand);
            //    //if (LeapMotionControl.Instance.Hand_Right_IsCloseHand)
            //    //{
            //    //    collision.GetComponent<baseOnClick>().TriggerStay();
            //    //}
            //    collision.GetComponent<baseOnClick>().TriggerStay();
            //    IsComplete = true;
            //}).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<baseOnClick>())
        {
            collision.GetComponent<baseOnClick>().TriggerExit();
            tweener.Kill();
            image.fillAmount = 0;
            IsComplete = false;
        }
    }
}
