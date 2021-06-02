using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseOnClick : MonoBehaviour
{
    //进入
    [HideInInspector]
    public Action<baseOnClick> OnEnter;
    //出去
    [HideInInspector]
    public Action<baseOnClick> OnExit;
    //停留
    [HideInInspector]
    public Action<baseOnClick> OnStay;

    public virtual void TriggerEnter()
    {
        OnEnter?.Invoke(this);
    }

    public virtual void TriggerStay()
    {
        OnStay?.Invoke(this);
    }

    public virtual void TriggerExit()
    {
        OnExit?.Invoke(this);
    }
}
