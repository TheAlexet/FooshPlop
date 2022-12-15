using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullState : NonInteractiveState
{
    public PullState(FishingSM stateMachine) : base("Pull", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _sm.fishManager.FishingHook.forceNotSplash = false;
        _sm.fishManager.FishingHook.SetSplashFX(false);
        _sm.fishManager.FishingHook.gameObject.SetActive(false);

        _sm.fishManager.SetCanSpawn(false);
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
}
