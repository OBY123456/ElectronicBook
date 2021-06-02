using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class DirectoryTask : BaseTask
{
    public DirectoryTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<DirectoryPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<DirectoryPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
