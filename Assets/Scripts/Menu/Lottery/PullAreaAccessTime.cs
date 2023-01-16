using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PullAreaAccessTime
{

    public PullAreaAccessTime() { }

    public void UpdateAccessTimes(AreaPassSO accessPass)
    {
        foreach (int areaInt in accessPass.areasUnlocked)
        {
            Database.IncrAccessTimeArea($"level{areaInt}", (int)accessPass.validityTime);
        }
    }
}
