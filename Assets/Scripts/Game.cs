using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    float gameTime = 60; //In seconds
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
        controlChrono();
    }

    void controlChrono()
    {
        if(!pause)
        {
            if(gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                updateChronoLabel(gameTime);
            }
            else
            {
                gameTime = 0;
                pause = true;
            }
        }
    }

    void updateChronoLabel(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        chronoText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
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
