using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public float fishScale = 1f;
    public GameObject SpawnFish(GameObject fish, PolygonArea fishArea)
    {
        GameObject curFish = GameObject.Instantiate(fish);
        fish.GetComponent<FishSM>().FishArea = fishArea;
        curFish.transform.position = fishArea.RandomPoint();
        curFish.transform.localScale = fishScale * curFish.transform.localScale;
        return curFish;
    }
}
