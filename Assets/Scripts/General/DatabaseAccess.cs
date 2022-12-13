using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAccess
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

    public int getFishCaught(string fishName)
    {
        return PlayerPrefs.GetInt(fishName, 0);
    }

    public void addFishCaught(string fishName)
    {
        PlayerPrefs.SetInt(fishName, getFishCaught(fishName) + 1);
    }

    public void SetCurrentHatIndex(int hatIndex) { PlayerPrefs.SetInt("hatIndex", hatIndex); }
    public int GetCurrentHatIndex() { return PlayerPrefs.GetInt("hatIndex", 0); }
    public void SetCurrentRodIndex(int rodIndex) { PlayerPrefs.SetInt("rodIndex", rodIndex); }
    public int GetCurrentRodIndex() { return PlayerPrefs.GetInt("rodIndex", 0); }

    public void SetCurrentSlipIndex(int slipIndex) { PlayerPrefs.SetInt("slipIndex", slipIndex); }

    public int GetCurrentSlipIndex() { return PlayerPrefs.GetInt("slipIndex", 0); }

}
