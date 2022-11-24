public class ScrollLevels : ScrollSystem
{
    public override string GetText()
    {
        return "Level " + closestPosition.ToString();
    }
}