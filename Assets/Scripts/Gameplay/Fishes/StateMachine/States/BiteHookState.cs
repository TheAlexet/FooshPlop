using UnityEngine;

public class BiteHookState : FishState
{
    public BiteHookState(FishSM stateMachine) : base("BiteHook", stateMachine) { }
    private float timeSinceBitten;
    private float delayBeforeCanLeave;

    public override void Enter()
    {
        base.Enter();
        _sm.IsBiting = true;

        timeSinceBitten = 0f;
        delayBeforeCanLeave = DelayBefore(_sm.Data.CatchBeforeDelay);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_sm.FishHead.HookBitten) { _sm.ChangeState(_sm.RandomState); }

        timeSinceBitten += Time.deltaTime;
        if (timeSinceBitten > delayBeforeCanLeave)
            _sm.IsLeaving = true;
    }
}