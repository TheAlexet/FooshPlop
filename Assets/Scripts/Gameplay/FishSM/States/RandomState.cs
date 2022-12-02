using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomState : MovingState
{
    float _timeSinceLastChange;
    float _delayBeforeChange;
    bool _hookSeen;

    public RandomState(FishSM stateMachine) : base("Random", stateMachine)
    {
        _sm = (FishSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        RandomDestination();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Debug.Log(_sm.fishHead.hookSeen);
        if (_sm.fishHead.hookSeen)
            _sm.ChangeState(_sm.toHookState);

        _timeSinceLastChange += Time.deltaTime;
        if (_timeSinceLastChange > _delayBeforeChange)
            RandomDestination();
    }

    void RandomDestination()
    {
        destination = _sm.NextDestination();
        _timeSinceLastChange = 0f;
        _delayBeforeChange = _sm.DelayBefore(_sm.changeDelay);
    }
}
