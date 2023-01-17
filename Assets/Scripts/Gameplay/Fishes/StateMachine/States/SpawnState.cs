using UnityEngine;

public class SpawnState : FishState
{
    public SpawnState(FishSM stateMachine) : base("Spawn", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        // do spawning animation here !
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.ChangeState(_sm.RandomState);
    }
}
