using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lottery : MonoBehaviour
{
    public List<AreaPassSO> areaPasses;
    private PullAreaAccessTime pullPass;
    [SerializeField] private TMPro.TextMeshProUGUI lotteryText;
    public int LotteryPrice = 1;

    public Lottery()
    {
        pullPass = new PullAreaAccessTime();
    }

    public void PlayOnce()
    {
        //@RaffaelbdlTODO rods 
        if (Database.GetLotteryTickets() >= LotteryPrice)
        {
            Database.IncrLotteryTickets(-LotteryPrice);

            int res = GetReward();
            lotteryText.text = $"You got {areaPasses[res].passName}";
            pullPass.UpdateAccessTimes(areaPasses[res]);
        }
        else { lotteryText.text = "Not enough tickets :/"; }

    }

    public int GetReward()
    {
        List<float> logits = new List<float>();
        foreach (AreaPassSO pass in areaPasses) { logits.Add(pass.rarity); }

        int res = Categorical.Choice(logits);
        return res;
    }
}
