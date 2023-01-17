using System.Collections.Generic;
using UnityEngine;

public class GenerateFishContent : MonoBehaviour
{
    [SerializeField] List<GameObject> fishesGameObjects;
    [SerializeField] GameObject buttonGameObject;
    [SerializeField] Transform parent;
    [SerializeField] Vector3 scale;
    [SerializeField] Vector3 rotation;
    [SerializeField] Vector3 position;
    void Start()
    {
        foreach (GameObject fish in fishesGameObjects)
        {
            GameObject fishButton = GameObject.Instantiate(buttonGameObject, parent);
            fishButton.name = fish.name;

            GameObject fishOject = GameObject.Instantiate(fish, fishButton.transform.GetChild(0));
            // Destroy(fishOject.GetComponent<FishMovement>());
            Destroy(fishOject.GetComponent<FishOnSpawn>());
        }
    }
}
