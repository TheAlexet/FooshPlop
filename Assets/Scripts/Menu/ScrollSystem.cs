using UnityEngine;
using UnityEngine.UI;

public class ScrollSystem : MonoBehaviour
{
    public GameObject contentHolder;
    [SerializeField] MyScrollRect scrollRect;
    [SerializeField] TMPro.TextMeshProUGUI scrollText;

    [Header("Auto focus")]
    public bool autoFocus = false;
    [Tooltip("Step for returning to object in focus")] public float returnStep = 0.1f;
    [Tooltip("Delay before starting focus")] public float delayAfterUnpress = 0.3f;
    float timeSinceUnpress = 0f;
    float scrollValue = 0f;

    [Header("Change size")]
    [Tooltip("Size when object is not in focus")] public float reducedSize = 0.6f;
    Vector3 normalScale;

    private float[] itemPositions;
    public int closestPosition;

    void Start()
    {
        itemPositions = GetPositions();
        normalScale = contentHolder.transform.GetChild(0).GetChild(0).localScale;
    }

    void Update()
    {
        closestPosition = GetClosestPostion();
        if (autoFocus) { AutoFocus(); }
        scrollText.text = GetText();
        UpdateItemSize();
    }

    int GetClosestPostion()
    {
        float curPos = scrollRect.horizontalNormalizedPosition;
        float halfDistance = 0.5f / (contentHolder.transform.childCount - 1);

        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            float childPos = itemPositions[i];
            if (curPos - halfDistance < childPos && childPos <= curPos + halfDistance)
            {
                return i;
            }
        }
        return contentHolder.transform.childCount - 1;
    }

    float[] GetPositions()
    {
        float[] positions = new float[contentHolder.transform.childCount];
        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            positions[i] = (float)i / (float)(contentHolder.transform.childCount - 1);
        }
        return positions;
    }

    public virtual string GetText()
    {
        string text = "This should be overriden";
        return text;
    }

    void AutoFocus()
    {
        if (!Input.GetMouseButton(0) && !(Input.touchCount > 0))
        {
            timeSinceUnpress += Time.deltaTime;
            if (timeSinceUnpress > delayAfterUnpress)
            {
                scrollValue = itemPositions[closestPosition];
                scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                    scrollRect.horizontalNormalizedPosition, scrollValue, returnStep
                );
            }
        }
        else { timeSinceUnpress = 0f; }
    }

    void UpdateItemSize()
    {
        for (int i = 0; i < contentHolder.transform.childCount; i++)
        {
            if (i == closestPosition)
            {
                Vector3 curScale = contentHolder.transform.GetChild(i).GetChild(0).localScale;
                contentHolder.transform.GetChild(i).GetChild(0).localScale = Vector3.Lerp(
                    curScale, normalScale, returnStep
                );
            }
            else
            {
                Vector3 curScale = contentHolder.transform.GetChild(i).GetChild(0).localScale;
                contentHolder.transform.GetChild(i).GetChild(0).localScale = Vector3.Lerp(
                    curScale, reducedSize * normalScale, returnStep
                );
            }
        }
    }
}
