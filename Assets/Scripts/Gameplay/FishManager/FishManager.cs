using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class FishManager : MonoBehaviour
{
    [Header("Fish Manager Values")]
    [SerializeField] SceneData SceneData;
    [field: SerializeField] public PolygonArea FishArea { get; private set; }
    private List<GameObject> spawnableFishes;
    [field: SerializeField] public FishingHook FishingHook { get; private set; }


    private List<float> fishesSpawnRate;

    private float delayBeforeCanSpawn;
    private float timeSinceCanSpawn;
    private float timeSinceFishCaughtMenuActive = 0f;

    private GameObject currentFish;

    [SerializeField] private bool canSpawn;
    [SerializeField] private bool canDestroy;
    [SerializeField] GameObject fishCaughtMenu;
    [SerializeField] TMPro.TextMeshProUGUI fishCaughtName;
    [SerializeField] TMPro.TextMeshProUGUI fishCaughtAcorns;
    [SerializeField] AudioSource caughtSound;

    public bool isBiting { get; private set; }
    public bool isLeaving { get; private set; }

    void Awake()
    {
        spawnableFishes = SceneData.FishesInScene;
    }

    void Start()
    {
        fishesSpawnRate = new List<float>();
        foreach (GameObject fish in spawnableFishes)
        {
            float rate = fish.GetComponent<Fish>().Data.SpawnRate;
            fishesSpawnRate.Add(rate);
        }
    }

    void Update()
    {
        if(timeSinceFishCaughtMenuActive > 3f)
        {
            fishCaughtMenu.SetActive(false);
        }
        else
        {
            timeSinceFishCaughtMenuActive += Time.deltaTime;
            fishCaughtMenu.transform.localPosition = new Vector3(fishCaughtMenu.transform.localPosition.x, fishCaughtMenu.transform.localPosition.y + 0.5f, fishCaughtMenu.transform.localPosition.z - 0.5f);
        }

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
            delayBeforeCanSpawn = Random.Range(SceneData.SpawnDelay.x, SceneData.SpawnDelay.y);
        }
    }

    public void SetCanDestroy(bool value) { canDestroy = value; }

    public void DestroyCurrentFish() { Destroy(currentFish); }

    public Fish GetCurrentFish() { return currentFish.GetComponent<Fish>(); }

    GameObject ChooseFish() { return spawnableFishes[Categorical.Choice(fishesSpawnRate)]; }

    GameObject SpawnFish(GameObject fish, PolygonArea fishArea)
    {
        GameObject curFish = GameObject.Instantiate(fish);
        curFish.transform.position = fishArea.RandomPoint();
        curFish.transform.localScale = SceneData.FishScale * curFish.transform.localScale;
        curFish.GetComponent<FishSM>().fishArea = fishArea;
        return curFish;
    }

    public void ShowFishCaught()
    {
        caughtSound.Play();
        timeSinceFishCaughtMenuActive = 0f;
        fishCaughtMenu.transform.localPosition = new Vector3(0f, 100f, -9450f);
        fishCaughtMenu.SetActive(true);
        fishCaughtName.text = "+ 1 " + GetCurrentFish().Data.FancyName;
        fishCaughtAcorns.text = "+ " + (GetCurrentFish().Data.Rarity * 10).ToString() + " acorns";
    }
}
