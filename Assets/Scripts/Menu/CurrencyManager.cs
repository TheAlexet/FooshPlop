using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI acornsText;
    [SerializeField] TMPro.TextMeshProUGUI ticketsText;
    private int acorns;
    private int tickets;


    void Update()
    {
        if (acorns != Database.getAcorns())
        {
            acorns = Database.getAcorns();
            UpdateAcorns();
        }

        if (tickets != Database.GetLotteryTickets())
        {
            tickets = Database.GetLotteryTickets();
            UpdateTickets();
        }
    }

    public void UpdateAcorns() { acornsText.text = acorns.ToString(); }
    public void UpdateTickets() { ticketsText.text = tickets.ToString(); }

}
