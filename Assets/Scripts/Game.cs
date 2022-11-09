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

    [SerializeField]
    private GameObject fishHook;

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
            idlePhase();
        }
        else if(phase == "fishingCast1")
        {
            fishingCast1Phase();
        }
        else if(phase == "fishingCast2")
        {
            fishingCast2Phase();
        }
        else if(phase == "fishing")
        {
            fishingPhase();
        }
        else if(phase == "fishingRecover")
        {
            Invoke("fishingRecoverPhase", 3f);
        }
    }

    void idlePhase()
    {
        if(Input.gyro.rotationRateUnbiased.x > 3)
        {
            fishingAnimator.SetTrigger("ToFishingCast1");
            phase = "fishingCast1";
            prevTime = DateTime.Now;
        }
    }

    /**
    Fish hook min and max coordinates: 
        x (z = -1): [-2.7, 2.7] (Left-Right)
        x (z = -7,2): [-1.2, 1.2] (Left-Right)
        y: 0.15 (Height)
        z: [-1, -7,2] (Far)
    Gyroscope min and max values:
        x (influences Far): [-3, -7]
        y (influences Left-Right): [3, -3]
        z: nothing
    */
    void fishingCast1Phase()
    {
        if(Input.gyro.rotationRateUnbiased.x < -3)
        {
            fishingAnimator.SetTrigger("ToFishingCast2");
            float farValue = Math.Abs(Input.gyro.rotationRateUnbiased.x) - 3f; //Between 0 and > 4
            if(farValue > 4f) farValue = 4f; //Between 0 and 4
            farValue *= 1.55f; //(7,2 - 1) / (7 - 4) //Between 0 and 6,2
            float auxiliarFarValue = farValue;
            farValue = -1f -farValue; //Between -1 and -7,2
            float leftRightValue = Input.gyro.rotationRateUnbiased.x; //Between < -3 and > 3
            if(leftRightValue > 3) leftRightValue = 3;
            if(leftRightValue < -3) leftRightValue = -3;
            leftRightValue += 3f; //Between 0 and 6
            auxiliarFarValue *= (1.5f/6.2f); //(2.7 - 1.2) / (6.2 - 0) Between 0 and 1.5
            auxiliarFarValue += 1.2f; //Between 1.2 and 2.7
            leftRightValue *= ((auxiliarFarValue + auxiliarFarValue)/6); // (2.7 + 2.7) / (3 + 3) Between 0 and 5.4
            leftRightValue -= auxiliarFarValue; //Between [-2.7 - -1.2} and [2.7 - 1.2]
            fishHook.transform.position = new Vector3(leftRightValue, 10f, farValue);
            Invoke("throwFishingHook", 2.5f);
            print(Input.gyro.rotationRateUnbiased);
            phase = "fishingCast2";
        }
    }

    void throwFishingHook()
    {
        fishHook.transform.position = new Vector3(fishHook.transform.position.x, 0.15f, fishHook.transform.position.z);
    }

    void fishingCast2Phase()
    {
        fishingAnimator.SetTrigger("ToFishing");
        phase = "fishing";
    }

    void fishingPhase()
    {
        if(!fishBit && Input.gyro.rotationRateUnbiased.x > 3)
        {
            fishingAnimator.SetTrigger("ToFishingRecover");
            phase = "fishingRecover";
        }
        else
        {
            /*if((DateTime.Now - prevTime).TotalSeconds >= 0 && (DateTime.Now - prevTime).TotalSeconds <= 3)
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
            }*/
        }
    }

    void fishingRecoverPhase()
    {
        fishingAnimator.SetTrigger("ToFishingIdle");
        shakes = 0;
        fishHook.transform.position = new Vector3(0f, 10f, 0f);
        phase = "idle";
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
