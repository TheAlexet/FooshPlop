using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollLevels : ScrollSystem
{
    private List<Transform> miniScenesParents;

    private void Awake()
    {
        miniScenesParents = new List<Transform>();
    }

    public override string GetText()
    {
        return "Level " + (closestPosition + 1).ToString() + $" ({GetFormatTimeText()})";
    }

    private string GetFormatTimeText()
    {
        float accessTime = Database.GetAccessTimeArea($"level{closestPosition}");
        if (miniScenesParents.Count != db.LevelsCount)
        {
            for (int i = 0; i < db.LevelsCount; i++)
            {
                miniScenesParents.Add(contentHolder.transform.GetChild(i).transform);
            }
        }

        if (accessTime < 0) { return "Unlocked !"; }
        if (accessTime == 0)
        {
            ChangeColors(closestPosition, false);
            return "Locked :/";
        }
        ChangeColors(closestPosition, true);
        if (accessTime < 60) { return $"{(int)accessTime} s"; }
        if (accessTime < 3600) { return $"{(int)accessTime / 60} min"; }
        if (accessTime < 86400) { return $"{(int)accessTime / 60 / 60} h"; }

        return $"{(int)accessTime / 60 / 60 / 60} days";
    }

    private void ChangeColors(int sceneInt, bool isUnlocked)
    {
        if (sceneInt == 0) return;
        miniScenesParents[sceneInt].GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(isUnlocked);
        miniScenesParents[sceneInt].GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(!isUnlocked);
    }
}