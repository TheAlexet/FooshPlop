using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public PolygonArea fishArea;
    public float moveSpeed;
    public float rotationSpeed = 1f;
    public float thresholdDist = 0.1f;
    public float thresholdAngle = 2f;
    public float minDelayBeforeChangeDestination = 1f;
    public float maxDelayBeforeChangeDestination = 5f;

    float timeSinceLastChangeDestination;
    float delayBeforeChangeDestination;
    [SerializeField] Vector3 destination;
    bool hookSeen = false;

    public void SetFishArea(PolygonArea area) { fishArea = area; }

    void OnEnable()
    {
        NextDestination();
        timeSinceLastChangeDestination = 0f;
        delayBeforeChangeDestination = Random.Range(minDelayBeforeChangeDestination, maxDelayBeforeChangeDestination);
    }

    void Update()
    {
        if (hookSeen) //If the fish has seen the hook
        {
            GoTowardDestination();
            if (GameObject.Find("FishingHook") == null)
            {
                hookSeen = false;
            }
        }
        else //If the fish has not seen the hook
        {
            GoTowardDestination();
            timeSinceLastChangeDestination += Time.deltaTime;
            if (timeSinceLastChangeDestination > delayBeforeChangeDestination)
            {
                NextDestination();
                timeSinceLastChangeDestination = 0f;
                delayBeforeChangeDestination = Random.Range(minDelayBeforeChangeDestination, maxDelayBeforeChangeDestination);
            }
        }
    }

    void NextDestination()
    {
        destination = fishArea.RandomPoint();
    }

    void GoTowardDestination()
    {
        Vector3 direction = (destination - transform.position);
        Quaternion lookDirection = Quaternion.LookRotation(direction.normalized);
        if (Mathf.Abs(Quaternion.Angle(transform.rotation, lookDirection)) > thresholdAngle)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, Time.deltaTime * rotationSpeed);

        }

        Vector3 displacement = direction.normalized * Time.deltaTime * moveSpeed;
        if (direction.magnitude > thresholdDist)
        {
            transform.Translate(displacement, Space.World);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "HookVisionField")
        {
            hookSeen = true;
            //    destination = new Vector3(0f, 0f, 0f);
            destination = col.transform.position;
        }
        if (col.gameObject.tag == "Hook")
        {
            GameObject.Find("RodManager").GetComponent<RodController>().fishBitHook = true;
            GameObject.Find("RodManager").GetComponent<RodController>().fishBitten = gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "HookVisionField")
        {
            hookSeen = false;
        }
    }
}
