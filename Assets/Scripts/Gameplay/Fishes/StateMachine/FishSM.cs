using UnityEngine;

public class FishSM : StateMachine
{
    public SpawnState SpawnState { get; }
    public RandomState RandomState { get; }
    public ToHookState ToHookState { get; }
    public BiteHookState BiteHookState { get; }

    public FishSM()
    {
        SpawnState = new SpawnState(this);
        RandomState = new RandomState(this);
        ToHookState = new ToHookState(this);
        BiteHookState = new BiteHookState(this);
    }

    [field: SerializeField] public Fish Fish { get; private set; }
    public FishData Data { get; private set; }
    [HideInInspector] public PolygonArea FishArea;
    public FishHead FishHead { get; private set; }

    public bool IsBiting;
    public bool IsLeaving;

    void Awake()
    {
        Data = Fish.Data;
        FishHead = GetComponentInChildren<FishHead>();
    }
    protected override BaseState GetInitialState() { return SpawnState; }
}
