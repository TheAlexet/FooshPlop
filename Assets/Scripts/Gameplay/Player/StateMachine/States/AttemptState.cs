using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptState : InteractiveState
{
    public AttemptState(FishingSM stateMachine) : base("Attempt", stateMachine) { }

    public override void Exit()
    {
        base.Exit();
        _sm.fishManager.DestroyCurrentFish();
        _sm.fishManager.FishingHook.SetSplashFX(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gyroRotationRate > 3f)
        {
            _sm.retrieveSound.Play();
            Database.setAcorns(Database.getAcorns() + _sm.fishManager.GetCurrentFish().Data.Rarity * 10);
            Database.addFishCaught(_sm.fishManager.GetCurrentFish().Data.FancyName);
            _sm.fishManager.ShowFishCaught();
            _sm.playerAnimator.SetTrigger("Pull");
            _sm.ChangeState(_sm.pullState);
        }

        if (_sm.fishManager.isLeaving)
        {
            _sm.playerAnimator.ResetTrigger("Hook");
            _sm.playerAnimator.SetTrigger("Idle");
            _sm.ChangeState(_sm.idleState);
        }
    }
}
