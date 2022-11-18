using UnityEngine;
using UnityEngine.UI;

public class ScrollLevels : MonoBehaviour
{
    [SerializeField] GameObject contentHolder;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    public float looseStep = 0.1f;
    public float reducedSize = 0.6f;

    [Header("Debug Values")]
    [SerializeField] int closestLevel = 0;
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
        if (!Input.GetMouseButton(0) && !(Input.touchCount > 0))
        {
            closestLevel = GetClosestLevel();
            UpdateTextLevel();
            scrollValue = positions[closestLevel];
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                scrollRect.horizontalNormalizedPosition, scrollValue, looseStep
            );
        }
        UpdateLevelSize();
    }

    int GetClosestLevel()
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

    void UpdateLevelSize()
    {
        int closestLevel = GetClosestLevel();
        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            if (i == closestLevel)
            {
                Vector3 curScale = contentHolder.transform.GetChild(i).GetChild(0).localScale;
                contentHolder.transform.GetChild(i).GetChild(0).localScale = Vector3.Lerp(
                    curScale, normalScale, looseStep
                );
            }
            else
            {
                Vector3 curScale = contentHolder.transform.GetChild(i).GetChild(0).localScale;
                contentHolder.transform.GetChild(i).GetChild(0).localScale = Vector3.Lerp(
                    curScale, reducedSize * normalScale, looseStep
                );
            }
        }
    }

    void UpdateTextLevel()
    {
        levelText.text = "Level " + closestLevel.ToString();
    }

}
