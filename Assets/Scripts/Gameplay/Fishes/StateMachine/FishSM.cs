using UnityEngine;

public class FishSM : StateMachine
{
    public SpawnState spawnState { get; }
    public RandomState randomState { get; }
    public ToHookState toHookState { get; }
    public BiteHookState biteHookState { get; }

    public FishSM()
    {
        spawnState = new SpawnState(this);
        randomState = new RandomState(this);
        toHookState = new ToHookState(this);
        biteHookState = new BiteHookState(this);
    }

    [field: SerializeField] public FishData Data { get; private set; }
    [HideInInspector] public PolygonArea fishArea;

    [field: SerializeField] public FishHead fishHead { get; private set; }

    public bool isBiting;
    public bool isLeaving;

    protected override BaseState GetInitialState() { return spawnState; }
}
