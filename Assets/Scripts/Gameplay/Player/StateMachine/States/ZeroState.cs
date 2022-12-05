using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroState : InteractiveState
{
    public ZeroState(FishingSM stateMachine) : base("Zero", stateMachine) { }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // . transition to "Cast" state if input < -3
        if (_gyroRotationRate < -3f)
        {
            _sm.playerAnimator.SetTrigger("Cast");
            stateMachine.ChangeState(_sm.castState);
        }
    }

}
