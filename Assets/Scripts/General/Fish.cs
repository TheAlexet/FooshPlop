using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [field: SerializeField] public FishData Data { get; private set; }
    [SerializeField] private bool useNewBones;

    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("isNewBones", useNewBones);
    }
}
