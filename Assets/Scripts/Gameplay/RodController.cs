using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{

    [SerializeField]
    private Animator fishingAnimator;

    [SerializeField]
    private GameObject fishingHook;

    [SerializeField]
    private Database db;

    [SerializeField]
    private FishManager fishManager;
    public Vector3 position;
    public Vector3 hookOffset;

    public bool fishBitHook = false;
    public GameObject fishBitten;
    float timeToCatch = 2f;
    float remainingTimeToCatch = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkRodState();
    }

    void checkRodState()
    {
        if (IsStateName("Zero"))
        {
            doStateNotFishing();
        }
        else if (IsStateName("Idle"))
        {
            doStateFishing();
        }
        else if (IsStateName("Hook"))
        {
            doStateHook();
        }
        else if (IsStateName("Catch"))
        {
            doStateCatch();
        }
    }

    void doStateNotFishing()
    {
        if (Input.gyro.rotationRateUnbiased.x < -3)
        {
            StartCoroutine(changeState("Cast"));
            fishingHook.SetActive(true);
            fishingHook.transform.position = position;
        }
    }

    void doStateFishing()
    {
        if (fishBitHook)
        {
            fishBitHook = false;
            timeToCatch = timeToCatch / fishBitten.GetComponent<FishData>().rarity;
            print(timeToCatch / fishBitten.GetComponent<FishData>().rarity);
            fishingHook.transform.position = position + hookOffset;
            StartCoroutine(changeState("FishCaught"));
        }
        else if (Input.gyro.rotationRateUnbiased.x > 3)
        {
            fishingHook.SetActive(false);
            StartCoroutine(changeState("Catch"));
        }
    }

    void doStateHook()
    {
        remainingTimeToCatch += Time.deltaTime;
        if (remainingTimeToCatch > timeToCatch) //Fish not caught in time
        {
            timeToCatch = 2f;
            remainingTimeToCatch = 0;
            fishManager.fishCount -= 1;
            if (fishBitten != null)
            {
                Destroy(fishBitten);
            }
            fishingHook.SetActive(false);
            StartCoroutine(changeState("Catch"));
        }
        else if (Input.gyro.rotationRateUnbiased.x > 3) //Fish caught in time
        {
            int acornsWon = fishBitten.GetComponent<FishData>().rarity * 10;
            db.setAcorns(db.getAcorns() + acornsWon);
            print("Total acorns: " + db.getAcorns().ToString());
            timeToCatch = 2f;
            remainingTimeToCatch = 0;
            fishManager.fishCount -= 1;
            if (fishBitten != null)
            {
                Destroy(fishBitten);
            }
            fishingHook.SetActive(false);
            StartCoroutine(changeState("Catch"));
        }
    }

    void doStateCatch()
    {
        fishingHook.SetActive(false);
    }

    bool IsStateName(string name)
    {
        return fishingAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    IEnumerator changeState(string trigger)
    {
        fishingAnimator.SetTrigger(trigger);
        yield return new WaitForSeconds(1);
        fishingAnimator.ResetTrigger(trigger);
    }
}
