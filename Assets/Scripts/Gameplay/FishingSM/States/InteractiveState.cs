using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveState : BaseState
{
    protected FishingSM _sm;
    protected float _gyroRotationRate;

    public InteractiveState(string name, FishingSM stateMachine) : base(name, stateMachine)
    {
        _sm = (FishingSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Input.gyro.enabled = true;
        _gyroRotationRate = Input.gyro.rotationRateUnbiased.x;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _gyroRotationRate = Input.gyro.rotationRateUnbiased.x;
    }

}
