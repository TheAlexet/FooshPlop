using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScrollRowVertical : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    public int itemsPerRow = 3;

    [SerializeField] List<GameObject> fishesGameObjects;
    [SerializeField] List<GameObject> fishesButtons;
    [SerializeField] GameObject buttonGameObject;
    [SerializeField] Transform parent;
    GameObject last_row;
    List<GameObject> fishesInstantiated;
    [SerializeField] GameObject fishDataMenu;
    [SerializeField] TMPro.TextMeshProUGUI fishName;
    [SerializeField] TMPro.TextMeshProUGUI fishRarity;
    [SerializeField] TMPro.TextMeshProUGUI fishAcorns;
    [SerializeField] TMPro.TextMeshProUGUI fishCaught;
    [SerializeField] private AudioSource buttonSound;

    [SerializeField] private Vector3 itemRotationInMenu;


    void Start()
    {
        fishesButtons = new List<GameObject>();
        fishDataMenu.SetActive(false);
        for (int i = 0; i < fishesGameObjects.Count; i++)
        {
            if (i % itemsPerRow == 0)
            {
                last_row = GameObject.Instantiate(rowPrefab, parent);
                last_row.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    last_row.GetComponent<RectTransform>().sizeDelta.x,
                    last_row.GetComponent<RectTransform>().sizeDelta.y / (float)(itemsPerRow - 1)
                );
                last_row.GetComponent<HorizontalLayoutGroup>().spacing = last_row.GetComponent<RectTransform>().sizeDelta.x / itemsPerRow / 10f;
            }
            GameObject fishButton = GameObject.Instantiate(buttonGameObject, last_row.transform);
            fishButton.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 150);
            GameObject fish = fishesGameObjects[i];
            GameObject fishOject = GameObject.Instantiate(fish, fishButton.transform.GetChild(0));
            fishOject.transform.localScale /= (itemsPerRow - 1);
            fishOject.transform.localEulerAngles = itemRotationInMenu;
            fishOject.transform.localPosition += fishOject.GetComponent<Fish>().Data.Offset;
            Destroy(fishOject.GetComponent<FishSM>());
            fishesButtons.Add(fishButton);
        }
        int emptyItems = itemsPerRow - fishesGameObjects.Count % itemsPerRow;
        for (int i = 0; i < emptyItems; i++)
        {
            GameObject fishButton = GameObject.Instantiate(buttonGameObject, last_row.transform);
            fishButton.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 150);

        }
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                if (i * 3f + j + 1f <= fishesGameObjects.Count)
                {
                    Transform grandChild = child.GetChild(j);
                    grandChild.GetComponent<Button>().onClick.AddListener(delegate { ShowFishData(grandChild.GetChild(0).GetChild(0).GetComponent<Fish>().Data); });
                }
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < fishesButtons.Count; i++)
        {
            if (fishesButtons[i].transform.position.y > 1.6f || fishesButtons[i].transform.position.y < 0.55)
            {
                fishesButtons[i].SetActive(false);
            }
            else
            {
                fishesButtons[i].SetActive(true);
            }
        }
    }

    public void ShowFishData(FishData fishData)
    {
        buttonSound.Play();
        fishDataMenu.SetActive(true);
        fishName.text = fishData.FancyName;
        fishRarity.text = fishData.Rarity.ToString();
        fishAcorns.text = (fishData.Rarity * 10f).ToString();
        fishCaught.text = Database.getFishCaught(fishData.FancyName).ToString();
    }

    public void CloseFishDataMenu()
    {
        fishDataMenu.SetActive(false);
        buttonSound.Play();
    }
}