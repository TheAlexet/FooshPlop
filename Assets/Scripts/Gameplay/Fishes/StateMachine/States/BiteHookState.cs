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

        timeSinceBitten += Time.deltaTime;
        if (timeSinceBitten > delayBeforeCanLeave)
            _sm.isLeaving = true;
    }
}