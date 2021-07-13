﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class LogoPanel : BasePanel
{
    protected override void Start()
    {
        base.Start();
        if(Config.Instance)
        {
            if(Config.Instance.configData.Logo.Contains("aabb"))
            {
                Hide();
            }
        }
    }
}
