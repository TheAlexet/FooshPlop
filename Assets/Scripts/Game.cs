using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Player player;
    string menu = "main";

    [SerializeField]
    private Database db;

    [SerializeField]
    private Text acornsText;

    [SerializeField]
    private GameObject characterMenu;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject fishMenu;

    // Start is called before the first frame update
    void Start()
    {
       initializePlayer();
       initializeAcornsText();
       //initializeLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializePlayer()
    {
        player = new Player();
        player.setAcorns(db.getAcorns());
        player.setMaxLevel(db.getMaxLevel());
    }

    void initializeAcornsText()
    {
        acornsText.text = player.getAcorns().ToString();
    }

    void initializeLevel()
    {
        string levelName = "Level" + player.getMaxLevel().ToString();
        GameObject instantiatedLevel = Instantiate((GameObject)Resources.Load(levelName), new Vector3(0, 0, 0), Quaternion.identity);
        instantiatedLevel.transform.SetParent(GameObject.Find("MainMenu").transform);
    }

    public void changeMenu(string newMenu)
    {
        if(menu != newMenu)
        {
            getMenu(menu).SetActive(false);
            getMenu(newMenu).SetActive(true);
            menu = newMenu;
        }
    }

    private GameObject getMenu(string menuName)
    {
        switch(menuName)
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
