using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    Vector3 hookPosition;
    public Vector3 hookOffset;

    public bool fishBitHook = false;
    public GameObject fishBitten;
    float timeToCatch = 2f;
    float remainingTimeToCatch = 0f;

    public string zeroState = "Zero";
    public string idleState = "Idle";
    public string hookState = "Hook";
    public string catchState = "Catch";
    public string castState = "Cast";
    public string notCaughtState = "NoCaught";

    public GameObject splashFX;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        hookPosition = fishingHook.transform.position;
        splashFX.SetActive(false);
        fishingHook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkRodState();
    }

    void checkRodState()
    {
        if (IsStateName(zeroState))
        {
            doStateNotFishing();

        }
        else if (IsStateName(idleState))
        {

            if (!fishingHook.activeSelf)
            {
                fishingHook.SetActive(true);
                fishingHook.transform.position = hookPosition;

            }
            doStateFishing();
        }
        else if (IsStateName(hookState))
        {
            doStateHook();
        }
        else if (IsStateName(catchState))
        {
            doStateCatch();

        }
    }

    void doStateNotFishing()
    {
        if (Input.gyro.rotationRateUnbiased.x < -3)
        {
            StartCoroutine(changeState(castState));
            // fishingHook.SetActive(true);
            // fishingHook.transform.position = hookPosition;
        }
    }

    void doStateFishing()
    {
        if (fishBitHook)
        {
            fishBitHook = false;
            timeToCatch = timeToCatch / fishBitten.GetComponent<FishData>().Rarity;
            print(timeToCatch / fishBitten.GetComponent<FishData>().Rarity);
            fishingHook.transform.position = hookPosition + hookOffset;
            splashFX.SetActive(true);
            StartCoroutine(changeState(hookState));
        }
        else if (Input.gyro.rotationRateUnbiased.x > 3)
        {
            fishingHook.SetActive(false);
            StartCoroutine(changeState(catchState));
        }
    }

    void doStateHook()
    {
        remainingTimeToCatch += Time.deltaTime;
        if (remainingTimeToCatch > timeToCatch) //Fish not caught in time
        {
            timeToCatch = 2f;
            remainingTimeToCatch = 0;
            // fishManager.fishCount -= 1;
            if (fishBitten != null)
            {
                Destroy(fishBitten);
            }

            // fishingHook.SetActive(false);
            // StartCoroutine(changeState(catchState));
            fishingHook.transform.position -= hookOffset;
            StartCoroutine(changeState(notCaughtState));
            splashFX.SetActive(false);
        }
        else if (Input.gyro.rotationRateUnbiased.x > 3) //Fish caught in time
        {
            int acornsWon = fishBitten.GetComponent<FishData>().Rarity * 10;
            db.setAcorns(db.getAcorns() + acornsWon);
            print("Total acorns: " + db.getAcorns().ToString());
            timeToCatch = 2f;
            remainingTimeToCatch = 0;
            // fishManager.fishCount -= 1;
            if (fishBitten != null)
            {
                Destroy(fishBitten);
            }
            fishingHook.SetActive(false);
            StartCoroutine(changeState(catchState));
            splashFX.SetActive(false);
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
