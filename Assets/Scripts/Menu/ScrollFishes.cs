public class ScrollFishes : ScrollSystem
{
    public override string GetText()
    {
        return contentHolder.transform.GetChild(closestPosition).GetComponentInChildren<FishData>().fancyName;
    }
}