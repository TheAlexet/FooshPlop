using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    int gameTime = 60; //In seconds
    float timeLeft;
    bool pause = true;    

    [SerializeField]
    private Database db;

    [SerializeField]
    private Text chronoText;

    [SerializeField]
    private AudioSource buttonSound;

    [SerializeField]
    private GameObject exitMenu;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            if(timeLeft > 0)
            {

            }
        }
    }

    public void exitHandler()
    {
        buttonSound.Play(); 
        if(!pause)
        {
            pause = true;
            exitMenu.SetActive(true);
            pauseGame();
        } 
        else 
        {
            pause = false;
            exitMenu.SetActive(false);
            resumeGame();
        }
    }

    public void exit()
    {
        buttonSound.Play();    
        SceneManager.LoadScene("Menu");
    }

    public void closeExitMenu()
    {
        buttonSound.Play(); 
        pause = false;
        exitMenu.SetActive(false);
        resumeGame();
    }

    void pauseGame()
    {
        if(!pause) 
        {
            pause = true;
        }
    }

    void resumeGame()
    {
        if(pause) 
        {
            pause = false;
        }
    }
}
