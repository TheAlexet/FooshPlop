using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : NonInteractiveState
{
    public CastState(FishingSM stateMachine) : base("Cast", stateMachine) { }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // transition to Idle if player animator is in Idle State
        // ie. if casting animation has finished
        if (_sm.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _sm.playerAnimator.ResetTrigger("Cast");
            _sm.ChangeState(_sm.idleState);
        }
    }
}
