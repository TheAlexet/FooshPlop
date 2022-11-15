using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    [SerializeField]
    private Animator fishingAnimator;

    private float timeSinceThrow = 0f;
    private float timeSinceCatch = 0f;
    public float actionDelay = 0.5f;

    [Header("States & Triggers Names")]
    public string notFishingState = "Zero";
    public string throwState = "Throw";
    public string idleState = "Idle";
    public string hookState = "Hook";
    public string catchState = "Catch";
    public string castTrigger = "Cast";
    public string fishCaughtTrigger = "FishCaught";
    public string catchTrigger = "Catch";


    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlFishingRod();
    }

    void ControlFishingRod()
    {
        if (IsStateName(notFishingState))
        {
            timeSinceCatch += Time.deltaTime;

            // Throws the line when gyro input
            if (timeSinceCatch > actionDelay && Input.gyro.rotationRateUnbiased.x < -3)
            {
                fishingAnimator.SetTrigger(castTrigger);
                fishingAnimator.ResetTrigger(catchTrigger);
                timeSinceThrow = 0f;
            }
        }

        else if (IsStateName(idleState))
        {
            timeSinceThrow += Time.deltaTime;

            // Gets back the line when gyro input
            if (timeSinceThrow > actionDelay && Input.gyro.rotationRateUnbiased.x > 3)
            {
                GetBackTheFloat();
            }
        }

        else if (IsStateName(hookState))
        {
            if (Input.gyro.rotationRateUnbiased.x > 3) { GetBackTheFloat(); }
        }
    }

    bool IsStateName(string name)
    {
        return fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    void GetBackTheFloat()
    {
        fishingAnimator.SetTrigger(catchTrigger);
        fishingAnimator.ResetTrigger(castTrigger);
        timeSinceCatch = 0f;
    }

}
