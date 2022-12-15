using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHead : MonoBehaviour
{
    public bool hookSeen;
    public bool hookBitten;
    public Vector3 hookPosition;

    void OnTriggerEnter(Collider col)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(col, gameObject, OnTriggerExit);
        switch (col.gameObject.tag)
        {
            case "HookInfluence":
                hookSeen = true;
                hookPosition = col.transform.position;
                break;

            case "Hook":
                hookBitten = true;
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(col, gameObject);
        switch (col.gameObject.tag)
        {
            case "HookInfluence":
                hookSeen = false;
                break;

            case "Hook":
                hookBitten = false;
                break;
        }
    }
}
