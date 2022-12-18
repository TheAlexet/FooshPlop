using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSM : StateMachine
{
    [HideInInspector] public ZeroState zeroState;
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public CastState castState;
    [HideInInspector] public PullState pullState;
    [HideInInspector] public AttemptState attemptState;

    public Animator playerAnimator;
    public FishManager fishManager;
    public DatabaseAccess db;
    public AudioSource throwSound;
    public AudioSource retrieveSound;

    private void Awake()
    {
        db = new DatabaseAccess();

        zeroState = new ZeroState(this);
        idleState = new IdleState(this);
        castState = new CastState(this);
        pullState = new PullState(this);
        attemptState = new AttemptState(this);
    }

    protected override BaseState GetInitialState()
    {
        return zeroState;
    }
}
