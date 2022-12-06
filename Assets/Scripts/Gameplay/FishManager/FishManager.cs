using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class FishManager : MonoBehaviour
{
    [Header("Fish Manager Values")]
    [SerializeField][Range(0.5f, 2f)] private float fishScale;
    [SerializeField][MinMaxSlider(0f, 5f)] private Vector2 spawnDelay;
    [field: SerializeField] public PolygonArea FishArea { get; private set; }
    [SerializeField] private List<GameObject> spawnableFishes;
    [field: SerializeField] public FishingHook FishingHook { get; private set; }


    private List<float> fishesSpawnRate;

    private float delayBeforeCanSpawn;
    private float timeSinceCanSpawn;

    private GameObject currentFish;

    [SerializeField] private bool canSpawn;
    [SerializeField] private bool canDestroy;

    public bool isBiting { get; private set; }
    public bool isLeaving { get; private set; }

    void Start()
    {
        fishesSpawnRate = new List<float>();
        foreach (GameObject fish in spawnableFishes)
        {
            float rate = fish.GetComponent<FishSM>().Data.SpawnRate;
            fishesSpawnRate.Add(rate);
        }
    }

    void Update()
    {
        if (canSpawn && currentFish == null)
        {
            timeSinceCanSpawn += Time.deltaTime;
            if (timeSinceCanSpawn > delayBeforeCanSpawn)
            {
                currentFish = SpawnFish(ChooseFish(), FishArea);
                canSpawn = false;
            }
        }

        if (currentFish != null)
        {
            isBiting = currentFish.GetComponent<FishSM>().isBiting;
            isLeaving = currentFish.GetComponent<FishSM>().isLeaving;
        }
        else
        {
            isBiting = false;
            isLeaving = true;
        }

        if (isBiting)
            FishingHook.SetSplashFX(true);
    }

    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
        if (canSpawn)
        {
            timeSinceCanSpawn = 0f;
            delayBeforeCanSpawn = Random.Range(spawnDelay.x, spawnDelay.y);
        }
    }

    public void SetCanDestroy(bool value) { canDestroy = value; }

    public void DestroyCurrentFish() { Destroy(currentFish); }

    GameObject ChooseFish()
    {
        return spawnableFishes[Categorical.Choice(fishesSpawnRate)];
    }

    GameObject SpawnFish(GameObject fish, PolygonArea fishArea)
    {
        GameObject curFish = GameObject.Instantiate(fish);
        curFish.transform.position = fishArea.RandomPoint();
        curFish.transform.localScale = fishScale * curFish.transform.localScale;
        curFish.GetComponent<FishSM>().fishArea = fishArea;
        return curFish;
    }
}
