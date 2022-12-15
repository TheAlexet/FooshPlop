using UnityEngine;

public class BiteHookState : FishState
{
    public BiteHookState(FishSM stateMachine) : base("BiteHook", stateMachine) { }
    private float timeSinceBitten;
    private float delayBeforeCanLeave;

    public override void Enter()
    {
        base.Enter();
        _sm.isBiting = true;

        timeSinceBitten = 0f;
        delayBeforeCanLeave = DelayBefore(_sm.Data.CatchBeforeDelay);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_sm.fishHead.hookBitten) { _sm.ChangeState(_sm.randomState); }

        timeSinceBitten += Time.deltaTime;
        if (timeSinceBitten > delayBeforeCanLeave)
            _sm.isLeaving = true;
    }
}