using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    Player player;
    string menu = "main";


    [SerializeField]
    private Text acornsText;

    [SerializeField]
    private GameObject characterMenu;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject fishMenu;

    [SerializeField]
    private AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        initializePlayer();
        initializeAcornsText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void initializePlayer()
    {
        player = new Player();
        player.setAcorns(Database.getAcorns());
        player.setMaxLevel(Database.getMaxLevel());
    }

    void initializeAcornsText()
    {
        acornsText.text = player.getAcorns().ToString();
    }

    public void changeMenu(string newMenu)
    {
        if (menu != newMenu)
        {
            getMenu(menu).SetActive(false);
            getMenu(newMenu).SetActive(true);
            menu = newMenu;
            buttonSound.Play();
            if (menu == "character")
            {
                GameObject.Find("CharacterButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                GameObject.Find("MainButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("FishButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else if (menu == "main")
            {
                GameObject.Find("CharacterButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("MainButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                GameObject.Find("FishButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else if (menu == "fish")
            {
                GameObject.Find("CharacterButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("MainButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("FishButton").transform.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
        }
    }

    private GameObject getMenu(string menuName)
    {
        switch (menuName)
        {
            case "character":
                return characterMenu;
            case "main":
                return mainMenu;
            case "fish":
                return fishMenu;
            default:
                return mainMenu;
        }
    }
}
