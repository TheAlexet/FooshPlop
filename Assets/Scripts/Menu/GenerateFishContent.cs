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
            fishOject.GetComponent<FishMovement>().enabled = false;
            fishOject.GetComponent<FishOnSpawn>().enabled = false;

            // fishButton.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(540f, -540f, 0f), Quaternion.identity);
            // // GameObject fishContent = new GameObject(fish.name);
            // fishContent.transform.SetParent(transform);

            // GameObject fishHolder = new GameObject("Fish");
            // fishHolder.transform.SetParent(fishContent.transform);

            // fishHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            // fishHolder.GetComponent<RectTransform>().SetPositionAndRotation(position, Quaternion.Euler(rotation));
            // fishHolder.GetComponent<RectTransform>().localScale = scale;

            // GameObject curFish = GameObject.Instantiate(fish);
            // curFish.transform.SetParent(fishHolder.transform);

            // curFish.GetComponent<FishOnSpawn>().enabled = false;
            // curFish.GetComponent<FishMovement>().enabled = false;
        }
    }
}
