using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    [SerializeField] private float timeToRefresh;

    // Make changes from editor only
    [field: SerializeField] public int Acorns { get; private set; }
    [field: SerializeField] public int LotteryTickets { get; private set; }
    [field: SerializeField] public int LevelsCount { get; private set; }
    [field: SerializeField] public List<float> LevelsTime { get; private set; }


    private void Awake()
    {
        InitializeLevelsTime();
    }

    private void Start()
    {
        StartCoroutine(IUpdateDatabase());
    }

    private void OnValidate()
    {
        if (LevelsTime.Count != LevelsCount) { InitializeLevelsTime(); }

        Database.setAcorns(Acorns);
        Database.SetLotteryTickets(LotteryTickets);

        for (int i = 0; i < LevelsCount; i++)
        {
            Database.SetAccessTimeArea($"level{i}", LevelsTime[i]);
        }

    }

    private void InitializeLevelsTime()
    {
        // Create LevelsTime
        LevelsTime = new List<float>();
        for (int i = 0; i < LevelsCount; i++) { LevelsTime.Add(Database.GetAccessTimeArea($"level{i}")); }
    }

    IEnumerator IUpdateDatabase()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToRefresh);
            UpdateAcorns();
            UpdateLotteryTickets();
            UpdateAccessTimes();
            UpdateTimeSinceStarted();
        }
    }

    #region Acorns
    public void UpdateAcorns()
    {
        Acorns = Database.getAcorns();
    }
    #endregion

    #region Lottery Tickets
    public void UpdateLotteryTickets()
    {
        LotteryTickets = Database.GetLotteryTickets();
    }
    #endregion 

    #region Access To Area
    public void UpdateAccessTimes()
    {
        for (int i = 0; i < LevelsCount; i++)
        {
            Database.IncrAccessTimeArea($"level{i}", -timeToRefresh);
            LevelsTime[i] = Database.GetAccessTimeArea($"level{i}");
        }

    }
    #endregion

    #region Keep Track of Time
    public void UpdateTimeSinceStarted()
    {
        Database.SetLastConnection(Time.time);
    }
    #endregion
}
