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
        timer += Time.deltaTime;
        // . transition to "Cast" state if input < -3
        if (_gyroRotationRate < -3f)
        {
            _sm.tutorialMenu.SetActive(false);
            _sm.throwSound.Play();
            _sm.playerAnimator.SetTrigger("Cast");
            stateMachine.ChangeState(_sm.castState);
        }
        else if(timer >= 10)
        {
            _sm.tutorialMenu.SetActive(true);
        }
        else if(Database.isFirstGame())
        {
            _sm.tutorialMenu.SetActive(true);
            Database.setFirstGame();
        }
    }

}
