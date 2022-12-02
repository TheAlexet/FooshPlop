using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullState : NonInteractiveState
{
    public PullState(FishingSM stateMachine) : base("Pull", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _sm.fishManager.fishingHook.forceNotSplash = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // transition to Zero if player animator is in Zero State
        // ie. if pulling animation has finished
        if (_sm.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zero"))
        {
            _sm.playerAnimator.ResetTrigger("Pull");
            _sm.ChangeState(_sm.zeroState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _sm.fishManager.fishingHook.forceNotSplash = false;
        _sm.fishManager.fishingHook.SetSplashFX(false);
        _sm.fishManager.fishingHook.gameObject.SetActive(false);
    }
}
