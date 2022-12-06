using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuFishManager : MonoBehaviour
{
    [SerializeField] List<GameObject> fishes;
    public PolygonArea fishArea;
    [SerializeField] FishSpawner fishSpawner;

    [SerializeField] bool fishExists = false;
    GameObject curFish;

    void Update()
    {
        if (!fishExists)
        {
            List<float> ones = new List<float>();
            foreach (GameObject f in fishes) { ones.Add(1.0f); }
            int fishInd = Categorical.Choice(ones);

            fishSpawner.SpawnFish(fishes[fishInd], fishArea);
            fishExists = true;
        }
    }

}
