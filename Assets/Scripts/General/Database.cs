using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public int getAcorns()
    {
        return PlayerPrefs.GetInt("acorns", 0);
    }

    public void setAcorns(int acorns)
    {
        PlayerPrefs.SetInt("acorns", acorns);
    }

    public int getMaxLevel()
    {
        return PlayerPrefs.GetInt("maxLevel", 1);
    }

    public void setMaxLevel(int maxLevel)
    {
        PlayerPrefs.SetInt("maxLevel", maxLevel);
    }

}
