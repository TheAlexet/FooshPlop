using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroState : InteractiveState
{
    public ZeroState(FishingSM stateMachine) : base("Zero", stateMachine) { }

    float timer = 0.0f;

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // . transition to "Cast" state if input < -3
        if (_gyroRotationRate < -3f)
        {
            _sm.tutorialMenu.SetActive(false);
            _sm.throwSound.Play();
            _sm.playerAnimator.SetTrigger("Cast");
            stateMachine.ChangeState(_sm.castState);
        }

        timer += Time.deltaTime;
        if(timer >= 10)
        {
            _sm.tutorialMenu.SetActive(true);
        }
    }

}
