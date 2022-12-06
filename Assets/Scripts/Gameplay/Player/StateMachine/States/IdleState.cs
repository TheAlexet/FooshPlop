using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : InteractiveState
{
    public IdleState(FishingSM stateMachine) : base("Idle", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _sm.fishManager.SetCanSpawn(true);
        _sm.fishManager.FishingHook.gameObject.SetActive(true);
        _sm.fishManager.FishingHook.SetSplashFX(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // receive message from fish

        // . transition to "Zero" state if input > 3
        if (_gyroRotationRate > 3f)
        {
            _sm.playerAnimator.SetTrigger("Pull");
            _sm.ChangeState(_sm.pullState);
        }

        if (_sm.fishManager.isBiting)
        {
            _sm.playerAnimator.ResetTrigger("Idle");
            _sm.playerAnimator.SetTrigger("Hook");
            _sm.ChangeState(_sm.attemptState);
        }
    }



}