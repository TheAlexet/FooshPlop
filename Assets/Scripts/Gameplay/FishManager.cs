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
    private bool fishExists = false;
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
        if (fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(zeroState))
        {
            GameObject.Destroy(curFish);
            fishExists = false;
        }
        // If throwing
        else if (fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(throwState))
        {
            timeSinceThrow = 0f;
            spawnDelay = Random.Range(2f, 10f);
        }
        // If waiting for fish
        else if (fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(idleState))
        {
            timeSinceThrow += Time.deltaTime;
            if (timeSinceThrow > spawnDelay && !fishExists)
            {
                curFish = fishSpawner.SpawnFish(ChooseFish(), fishArea);
                fishExists = true;
            }
        }
    }

    GameObject ChooseFish()
    {
        return spawnableFishes[new Categorical().Choice(fishesSpawnRate)];
    }
}
