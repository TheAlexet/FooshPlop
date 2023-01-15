using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyRewards : MonoBehaviour
{
    [SerializeField] private GameObject rewardMenu;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private int rewardHour = 6;
    [SerializeField] private int numTickets = 5;

    private DatabaseAccess db;

    private void Awake()
    {
        int lastConnection = Database.GetLastConnection();
        DateTime todayRewardTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, rewardHour, 0, 0);
        int todayUnixRewardTime = (int)todayRewardTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        int yesterdayUnixRewardTime = todayUnixRewardTime - 60 * 60 * 24;
        int nowTime = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;

        int timeToReward = nowTime - todayUnixRewardTime;
        if (timeToReward < 0)
        {
            if (nowTime - lastConnection > nowTime - yesterdayUnixRewardTime) { Reward(); }
        }
        else
        {
            if (nowTime - lastConnection > nowTime - todayUnixRewardTime) { Reward(); }
        }
    }

    public void CloseCanvas()
    {
        rewardMenu.SetActive(false);
        closeButton.SetActive(false);
    }

    public void Reward()
    {
        rewardMenu.SetActive(true);
        closeButton.SetActive(true);

        Database.IncrLotteryTickets(numTickets);
    }
}
