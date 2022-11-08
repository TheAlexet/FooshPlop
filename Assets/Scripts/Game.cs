using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    float gameTime = 60; //In seconds
    bool pause = true;
    int fishCaught = 0;
    int acornsWon = 0;

    [SerializeField]
    private Database db;

    [SerializeField]
    private Text chronoText;

    [SerializeField]
    private AudioSource levelSound;

    [SerializeField]
    private AudioSource buttonSound;

    [SerializeField]
    private GameObject exitMenu;

    [SerializeField]
    private GameObject finishMenu;

    [SerializeField]
    private Text finishResultText;

    [SerializeField]
    private Text fishCaughtText;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        controlFishingRod();
        controlChrono();
    }

    void controlFishingRod()
    {
        print(Input.acceleration);
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
                finishGame();
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

    void finishGame()
    {
        gameTime = 0;
        pauseGame();
        finishResultText.text = "Fish: " + fishCaught + "\nAcorns: " + acornsWon;
        db.setAcorns(db.getAcorns() + acornsWon);
        finishMenu.SetActive(true);
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
            levelSound.Pause();
        }
    }

    void resumeGame()
    {
        if(pause) 
        {
            pause = false;
            levelSound.Play();
        }
    }

    void fishCaughtHandler()
    {
        fishCaught += 1;
        acornsWon += 5;
        fishCaughtText.text = fishCaught.ToString();
    }
}
