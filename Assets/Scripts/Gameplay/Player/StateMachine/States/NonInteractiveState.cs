using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonInteractiveState : BaseState
{
    protected FishingSM _sm;

    public NonInteractiveState(string name, FishingSM stateMachine) : base(name, stateMachine)
    {
        _sm = (FishingSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Input.gyro.enabled = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

}
