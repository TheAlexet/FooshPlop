using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int acorns;
    private int maxLevel;

    public int getAcorns()
    {
        return acorns;
    }

    public void setAcorns(int newAcorns)
    {
        acorns = newAcorns;
    }

    public int getMaxLevel()
    {
        return maxLevel;
    }

    public void setMaxLevel(int newMaxLevel)
    {
        maxLevel = newMaxLevel;
    }
}
