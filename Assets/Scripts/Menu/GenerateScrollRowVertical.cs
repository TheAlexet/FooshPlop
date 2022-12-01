using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScrollRowVertical : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    public int itemsPerRow = 3;

    [SerializeField] List<GameObject> fishesGameObjects;
    [SerializeField] GameObject buttonGameObject;
    [SerializeField] Transform parent;
    GameObject last_row;

    void Start()
    {
        for (int i = 0; i < fishesGameObjects.Count; i++)
        {
            if (i % itemsPerRow == 0)
            {
                last_row = GameObject.Instantiate(rowPrefab, parent);
                last_row.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    last_row.GetComponent<RectTransform>().sizeDelta.x,
                    last_row.GetComponent<RectTransform>().sizeDelta.y / (float)(itemsPerRow - 1)
                );
                last_row.GetComponent<HorizontalLayoutGroup>().spacing = last_row.GetComponent<RectTransform>().sizeDelta.x / itemsPerRow;
            }
            GameObject fishButton = GameObject.Instantiate(buttonGameObject, last_row.transform);
            GameObject fish = fishesGameObjects[i];
            fishButton.name = fish.name;
            if(fishButton.GetComponent<Button>() != null)
            {
                print("Button found");    
                fishButton.GetComponent<Button>().onClick.AddListener(ShowFishData); 
            }
            GameObject fishOject = GameObject.Instantiate(fish, fishButton.transform.GetChild(0));
            fishOject.transform.localScale /= (itemsPerRow - 1);
            Destroy(fishOject.GetComponent<FishMovement>());
            Destroy(fishOject.GetComponent<FishOnSpawn>());

            
        }

        int emptyItems = itemsPerRow - fishesGameObjects.Count % itemsPerRow;
        for (int i = 0; i < emptyItems; i++)
        {
            GameObject fishButton = GameObject.Instantiate(buttonGameObject, last_row.transform);

        }
    }

    public void ShowFishData()
    {
        print("Fish Data:");
        print(gameObject.GetComponent<FishData>().fancyName);
    }
}
