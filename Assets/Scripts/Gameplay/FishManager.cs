using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField] private PolygonArea fishArea;
    [SerializeField] private List<GameObject> spawnableFishes;
    public float fishScale;
    [MinMaxSlider(0f, 10f)] public Vector2 spawnDelay;

    private List<float> fishesSpawnRate;
    [SerializeField] private bool canSpawn;
    [SerializeField] private bool canDestroy;
    [SerializeField] private float _timeCanSpawn;
    private float _delayBeforeSpawn;
    private GameObject currentFish;

    public bool isBiting;
    public bool isLeaving;
    public FishingHook fishingHook;

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
        if (canSpawn && currentFish == null)
        {
            _timeCanSpawn += Time.deltaTime;
            if (_timeCanSpawn > _delayBeforeSpawn)
            {
                currentFish = SpawnFish(ChooseFish(), fishArea);
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
            fishingHook.SetSplashFX(true);
    }

    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
        if (canSpawn)
        {
            _timeCanSpawn = 0f;
            _delayBeforeSpawn = Random.Range(spawnDelay.x, spawnDelay.y);
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
