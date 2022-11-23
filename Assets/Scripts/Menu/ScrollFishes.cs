using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollFishes : MonoBehaviour
{
    [SerializeField] GameObject contentHolder;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    public float looseStep = 0.1f;
    public float reducedSize = 0.6f;

    [Header("Debug Values")]
    [SerializeField] int closestFish = 0;
    [SerializeField] float scrollValue = 0f;
    [SerializeField] float[] positions;
    [SerializeField] Vector3 normalScale;

    void Start()
    {
        positions = new float[contentHolder.transform.childCount];
        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            positions[i] = (float)i / (contentHolder.transform.childCount - 1);
        }

        normalScale = contentHolder.transform.GetChild(0).GetChild(0).localScale;
    }


    void Update()
    {
        closestFish = GetClosestFish();
        UpdateFishText();
    }

    int GetClosestFish()
    {
        float curPos = scrollRect.horizontalNormalizedPosition;
        float halfDistance = 0.5f / (contentHolder.transform.childCount - 1);

        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            float childPos = positions[i];
            if (curPos - halfDistance < childPos && childPos <= curPos + halfDistance)
            {
                return i;
            }
        }
        return 0;
    }


    void UpdateFishText()
    {
        levelText.text = contentHolder.transform.GetChild(closestFish).GetComponentInChildren<FishData>().fancyName;
    }

}
