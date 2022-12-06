using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOnSpawn : MonoBehaviour
{
    [SerializeField] Animator fishAnimator;
    [SerializeField] FishData fishData;

    void Awake()
    {
        fishAnimator = GetComponent<Animator>();
        fishData = GetComponent<FishData>();
        fishAnimator.SetBool("isSSR", fishData.Rarity == 5);
    }
}
