using UnityEngine;

public class ToHookState : MovingState
{
    public ToHookState(FishSM stateMachine) : base("ToHook", stateMachine) { }

    #region State Methods
    public override void Enter()
    {
        base.Enter();
        destination = _sm.FishHead.HookPosition;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_sm.FishHead.HookSeen) _sm.ChangeState(_sm.RandomState);

        if (_sm.FishHead.HookBitten) _sm.ChangeState(_sm.BiteHookState);
    }
    #endregion
}
