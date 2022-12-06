using UnityEngine;

public class FishState : BaseState
{
    protected FishSM _sm;
    public FishState(string name, FishSM stateMachine) : base(name, stateMachine)
    {
        _sm = stateMachine;
    }

    #region Reusable Methods
    protected float DelayBefore(Vector2 delay) { return Random.Range(delay.x, delay.y); }
    #endregion
}
