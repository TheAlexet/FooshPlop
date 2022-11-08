using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    float gameTime = 60; //Duration of the game, in seconds
    bool pause = true; //If the game is paused
    int fishCaught = 0; //Fishes caught during the game
    int acornsWon = 0; //Acorns won during the game
    string phase = "idle"; //Fishing phase
    DateTime prevTime; //To calculate the time the player has for shaking
    int shakes = 0; //Number of shakes done
    bool positiveShake = true; //To count the shakes
    bool fishBit = false; //If the fish bit the hook

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

    [SerializeField]
    private Animator fishingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        controlFishingRod();
        controlChrono();
    }

    void controlFishingRod()
    {
        if(phase == "idle")
        {
            if(Input.gyro.rotationRateUnbiased.x > 3)
            {
                phase = "fishingCast1";
                fishingAnimator.SetTrigger("ToFishingCast1");
                prevTime = DateTime.Now;
            }
        }
        if(phase == "fishingCast1")
        {
            if(Input.gyro.rotationRateUnbiased.x < -3)
            {
                phase = "fishingCast2";
                fishingAnimator.SetTrigger("ToFishingCast2");
            }
        }
        if(phase == "fishingCast2")
        {
            phase = "fishing";
            //fishingAnimator.SetTrigger("ToFishing");
        }
        if(phase == "fishing")
        {
            if((DateTime.Now - prevTime).TotalSeconds >= 0 && (DateTime.Now - prevTime).TotalSeconds <= 3)
            {
                if(positiveShake && Input.gyro.rotationRateUnbiased.z > 3)
                {
                    positiveShake = false;
                    shakes += 1;
                    fishingAnimator.SetTrigger("ToFishingRecover");
                }
                else if(!positiveShake && Input.gyro.rotationRateUnbiased.z < -3)
                {
                    positiveShake = true;
                    shakes += 1;
                    fishingAnimator.SetTrigger("ToFishingRecover");
                }
                else if(!fishBit && shakes >= 20)
                {
                    phase = "fishingRecover";
                    fishingAnimator.SetTrigger("ToFishingRecover");
                }
            }
            else
            {
                prevTime = DateTime.Now;
                shakes = 0;
            }
        }
        if(phase == "fishingRecover")
        {
            shakes = 0;
            phase = "idle";
            fishingAnimator.SetTrigger("ToFishingIdle");
        }
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
