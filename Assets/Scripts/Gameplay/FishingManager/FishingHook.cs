using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHook : MonoBehaviour
{
    public GameObject splashFX;
    public bool forceNotSplash = false;
    public void SetSplashFX(bool value) { splashFX.SetActive(value && !forceNotSplash); }
}
