using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnableFishes;
    public List<float> fishesSpawnRate;



    private bool fishExists = false;
    private GameObject curFish;

    private float timeSinceThrow = 0f;

    public Animator fishingAnimator;
    public string zeroState = "Zero";
    public string idleState = "Idle";
    public string throwState = "Throw";

    private float spawnDelay;

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
            spawnDelay = Random.Range(2f, 10f);
        }
        // If waiting for fish
        else if (fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(idleState))
        {
            timeSinceThrow += Time.deltaTime;
            if (timeSinceThrow > spawnDelay && !fishExists) { SpawnFish(ChooseFish()); }
        }
    }

    int ChooseFish()
    {
        // Choose a fish given their spawnRate
        return new Categorical().Choice(fishesSpawnRate);
    }

    void SpawnFish(int fish)
    {
        curFish = GameObject.Instantiate(spawnableFishes[fish]);
        curFish.transform.position = new Vector3(
            Random.Range(-3f, 3f), 0.15f, Random.Range(-1.5f, -6.5f)
        );
        fishExists = true;
    }

}
