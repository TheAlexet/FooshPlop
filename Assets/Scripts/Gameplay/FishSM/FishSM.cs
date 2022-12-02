using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSM : StateMachine
{
    [HideInInspector] public SpawnState spawnState;
    [HideInInspector] public RandomState randomState;
    [HideInInspector] public ToHookState toHookState;
    [HideInInspector] public BiteHookState biteHookState;

    public PolygonArea fishArea;
    [MinMaxSlider(0f, 10f)] public Vector2 changeDelay;
    [MinMaxSlider(0f, 10f)] public Vector2 catchDelay;
    public float translateSpeed;
    public float rotateSpeed;
    public FishHead fishHead;
    public bool isBiting;
    public bool isLeaving;

    private void Awake()
    {
        spawnState = new SpawnState(this);
        randomState = new RandomState(this);
        toHookState = new ToHookState(this);
        biteHookState = new BiteHookState(this);
    }

    public Vector3 NextDestination() { return fishArea.RandomPoint(); }
    public float DelayBefore(Vector2 delay) { return Random.Range(delay.x, delay.y); }

    protected override BaseState GetInitialState()
    {
        return spawnState;
    }
}
