using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHookState : MovingState
{
    public ToHookState(FishSM stateMachine) : base("ToHook", stateMachine)
    {
        _sm = (FishSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        destination = _sm.fishHead.hookPosition;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_sm.fishHead.hookBitten)
            _sm.ChangeState(_sm.biteHookState);
    }
}
