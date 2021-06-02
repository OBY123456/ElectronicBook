using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeapButton : baseOnClick
{

    public override void TriggerEnter()
    {
        base.TriggerEnter();
    }

    public override void TriggerStay()
    {
        base.TriggerStay();
    }

    public override void TriggerExit()
    {
        base.TriggerExit();
    }

    public void BoxColliderOpen()
    {
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void BoxColliderHide()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }
}