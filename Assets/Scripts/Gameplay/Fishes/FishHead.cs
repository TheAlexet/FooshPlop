using UnityEngine;

public class FishHead : MonoBehaviour
{
    public bool HookSeen;
    public bool HookBitten;
    public Vector3 HookPosition;

    void OnTriggerEnter(Collider col)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(col, gameObject, OnTriggerExit);
        switch (col.gameObject.tag)
        {
            case "HookInfluence":
                HookSeen = true;
                HookPosition = col.transform.position;
                break;

            case "Hook":
                HookBitten = true;
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(col, gameObject);
        switch (col.gameObject.tag)
        {
            case "HookInfluence":
                HookSeen = false;
                break;

            case "Hook":
                HookBitten = false;
                break;
        }
    }
}
