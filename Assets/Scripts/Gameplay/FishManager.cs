using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [Header("Manager modules")]
    [SerializeField] FishSpawner fishSpawner; // Handles spawning fishes

    [Header("Scene information")]
    [SerializeField] PolygonArea fishArea; // Area where fishes spawn and move
    [SerializeField] List<GameObject> spawnableFishes; // List of spawnable fishes in the scene

    [Header("State information")]
    public Animator fishingAnimator;
    public string zeroState = "Zero";
    public string idleState = "Idle";
    public string throwState = "Throw";

    List<float> fishesSpawnRate;
    private int fishCountMax = 3;
    public int fishCount = 0;
    private GameObject curFish;
    private float timeSinceThrow = 0f;
    private float spawnDelay;


    void Start()
    {
        fishesSpawnRate = new List<float>();
        foreach (GameObject fish in spawnableFishes)
        {
            float rate = fish.GetComponent<FishData>().spawnRate;
            fishesSpawnRate.Add(rate);
        }
    }

    void Update()
    {
        // If doing nothing
        if (IsStateName("Zero"))
        {

        }
        // If throwing
        else if (IsStateName("Throw"))
        {
            timeSinceThrow = 0f;
            spawnDelay = Random.Range(2f, 10f);
        }
        // If waiting for fish
        else if (IsStateName("Idle"))
        {
            timeSinceThrow += Time.deltaTime;
            if (timeSinceThrow > spawnDelay && fishCount < fishCountMax)
            {
                curFish = fishSpawner.SpawnFish(ChooseFish(), fishArea);
                fishCount += 1;
                timeSinceThrow = 0f;
            }
        }
    }

    bool IsStateName(string name)
    {
        return fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    GameObject ChooseFish()
    {
        return spawnableFishes[new Categorical().Choice(fishesSpawnRate)];
    }
}
