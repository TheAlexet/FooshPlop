using UnityEngine;
using UnityEngine.UI;

public class ScrollLevels : ScrollSystem
{
    [SerializeField]
    Database db;

    public override string GetText()
    {
        if(db.getMaxLevel() > closestPosition)
        {
            return "Level " + (closestPosition + 1).ToString();
        }
        else
        {
            if(closestPosition + 1 < 3)
            {
                return (closestPosition * 100).ToString() + " acorns";
            }
            else
            {
                return "Coming soon";
            }
        }
    }
}