using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : BaseState
{
    protected FishSM _sm;

    public SpawnState(FishSM stateMachine) : base("Spawn", stateMachine)
    {
        _sm = (FishSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        // do spawning animation here !
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.ChangeState(_sm.randomState);
    }
}
