using UnityEngine;

public class RandomState : MovingState
{
    public RandomState(FishSM stateMachine) : base("Random", stateMachine) { }
    private float timeSinceLastChange;
    private float delayBeforeNextChange;

    #region State Methods
    public override void Enter()
    {
        base.Enter();
        ChangeRandomDestination();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_sm.fishHead.hookSeen)
        {
            _sm.ChangeState(_sm.toHookState);
            return;
        }

        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange > delayBeforeNextChange) ChangeRandomDestination();
    }
    #endregion

    #region Main Methods
    void ChangeRandomDestination()
    {
        destination = NextRandomDestination();
        timeSinceLastChange = 0f;
        delayBeforeNextChange = DelayBefore(_sm.Data.ChangeDestinationDelay);
    }
    #endregion
}
