using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Tickets()
    {
        Database.IncrLotteryTickets(10);
    }

    public void Acorns()
    {
        Database.IncrAcorns(100);
    }
}
