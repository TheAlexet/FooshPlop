using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{

    public static int getAcorns()
    {
        return PlayerPrefs.GetInt("acorns", 0);
    }

    public static void setAcorns(int acorns)
    {
        PlayerPrefs.SetInt("acorns", acorns);
    }

    public static void IncrAcorns(int acorns)
    {
        PlayerPrefs.SetInt("acorns", Mathf.Max(0, PlayerPrefs.GetInt("acorns") + acorns));
    }

    public static int getMaxLevel()
    {
        return PlayerPrefs.GetInt("maxLevel", 1);
    }

    public static void setMaxLevel(int maxLevel)
    {
        PlayerPrefs.SetInt("maxLevel", maxLevel);
    }

    public static int getFishCaught(string fishName)
    {
        return PlayerPrefs.GetInt(fishName, 0);
    }

    public static void addFishCaught(string fishName)
    {
        PlayerPrefs.SetInt(fishName, getFishCaught(fishName) + 1);
    }

    public static bool getPurchasedItem(string itemName)
    {
        if(PlayerPrefs.GetInt(itemName, 0) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void setPurchasedItem(string itemName)
    {
        PlayerPrefs.SetInt(itemName, 1);
    }

    public static bool isFirstGame()
    {
        if(PlayerPrefs.GetInt("firstGame", 0) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void setFirstGame()
    {
        PlayerPrefs.SetInt("firstGame", 1);
    }

    #region Lottery Tickets
    public static void SetLotteryTickets(int tickets) { PlayerPrefs.SetInt("lotteryTickets", tickets); }
    public static int GetLotteryTickets() { return PlayerPrefs.GetInt("lotteryTickets"); }
    public static void IncrLotteryTickets(int tickets)
    {
        PlayerPrefs.SetInt("lotteryTickets", Mathf.Max(0, PlayerPrefs.GetInt("lotteryTickets") + tickets));
    }
    #endregion

    #region Character Customization
    public static void SetCurrentHatIndex(int hatIndex) { PlayerPrefs.SetInt("hatIndex", hatIndex); }
    public static int GetCurrentHatIndex() { return PlayerPrefs.GetInt("hatIndex", 0); }
    public static void SetCurrentRodIndex(int rodIndex) { PlayerPrefs.SetInt("rodIndex", rodIndex); }
    public static int GetCurrentRodIndex() { return PlayerPrefs.GetInt("rodIndex", 0); }
    public static void SetCurrentSlipIndex(int slipIndex) { PlayerPrefs.SetInt("slipIndex", slipIndex); }
    public static int GetCurrentSlipIndex() { return PlayerPrefs.GetInt("slipIndex", 0); }
    #endregion

    #region Access Time Area
    public static void SetAccessTimeArea(string areaName, float accessTime)
    {
        PlayerPrefs.SetFloat(areaName, accessTime);
    }
    public static void IncrAccessTimeArea(string areaName, float accessTime)
    {
        float currentTime = PlayerPrefs.GetFloat(areaName);
        if (currentTime >= 0f)
        {
            PlayerPrefs.SetFloat(areaName, Mathf.Max(0f, currentTime + accessTime));
        }
    }
    public static float GetAccessTimeArea(string areaName)
    {
        return PlayerPrefs.GetFloat(areaName);
    }
    #endregion

    public static void SetLastConnection(float time)
    {
        PlayerPrefs.SetFloat("lastConnection", time);
    }
    public static float GetLastConnection()
    {
        return PlayerPrefs.GetFloat("lastConnection");
    }
}
