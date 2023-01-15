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

    [SerializeField] private GameObject popupMenu;
    [SerializeField] private TMPro.TextMeshProUGUI popupName;
    [SerializeField] private TMPro.TextMeshProUGUI popupText;
    [SerializeField] private AudioSource lotterySound;
    [SerializeField] private Vector3 popupStartPosition;
    [SerializeField] private Vector3 popupEndPosition;
    [SerializeField] private float popupStartAlpha;
    [SerializeField] private float popupEndAlpha;
    [SerializeField] private float timeSinceDisplay;


    public Lottery()
    {
        pullPass = new PullAreaAccessTime();
    }

    private void Update()
    {
        if (timeSinceDisplay > 3f)
        {
            popupMenu.SetActive(false);
        }
        else
        {
            timeSinceDisplay += Time.deltaTime;
            popupMenu.transform.localPosition = Vector3.Lerp(popupMenu.transform.localPosition, popupEndPosition, Time.deltaTime);

            Color popupMenuColor = popupMenu.GetComponent<Image>().color;
            float newAlpha = Mathf.Lerp(popupMenuColor.a, popupEndAlpha, Time.deltaTime);
            Color newColor = new Color(popupMenuColor.r, popupMenuColor.g, popupMenuColor.b, newAlpha);
            popupMenu.GetComponent<Image>().color = newColor;
        }
    }

    public void PlayOnce()
    {
        if (Database.GetLotteryTickets() >= LotteryPrice)
        {
            Database.IncrLotteryTickets(-LotteryPrice);

            int res = GetReward();
            DisplayPopUp("New Area Pass !", $"{areaPasses[res].passName}");
            pullPass.UpdateAccessTimes(areaPasses[res]);
            lotteryText.text = $"You got {areaPasses[res].passName}";
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

    public void DisplayPopUp(string name, string text)
    {
        timeSinceDisplay = 0f;
        lotterySound.Play();

        popupMenu.transform.localPosition = popupStartPosition;
        popupMenu.SetActive(true);

        Color popupMenuColor = popupMenu.GetComponent<Image>().color;
        Color startingColor = new Color(popupMenuColor.r, popupMenuColor.g, popupMenuColor.b, popupStartAlpha);
        popupMenu.GetComponent<Image>().color = startingColor;

        popupName.text = name;
        popupText.text = text;
    }
}
