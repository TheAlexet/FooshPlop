using UnityEngine;

public class ToHookState : MovingState
{
    public ToHookState(FishSM stateMachine) : base("ToHook", stateMachine) { }

    #region State Methods
    public override void Enter()
    {
        base.Enter();
        destination = _sm.fishHead.hookPosition;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_sm.fishHead.hookBitten) _sm.ChangeState(_sm.biteHookState);
    }
    #endregion
}
