using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuFishManager : MonoBehaviour
{
    [SerializeField] List<GameObject> fishes;
    [SerializeField] PolygonArea fishArea;
    [SerializeField] FishSpawner fishSpawner;

    bool fishExists = false;
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
