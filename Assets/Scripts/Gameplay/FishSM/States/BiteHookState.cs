using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteHookState : BaseState
{
    protected FishSM _sm;

    float _timeSinceBitten;
    float _delayBeforeLeave;

    public BiteHookState(FishSM stateMachine) : base("BiteHook", stateMachine)
    {
        _sm = (FishSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.isBiting = true;

        _timeSinceBitten = 0f;
        _delayBeforeLeave = _sm.DelayBefore(_sm.catchDelay);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _timeSinceBitten += Time.deltaTime;
        if (_timeSinceBitten > _delayBeforeLeave)
            _sm.isLeaving = true;
    }
}