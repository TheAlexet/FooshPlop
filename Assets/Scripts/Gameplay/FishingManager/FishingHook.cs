using UnityEngine;

public class FishingHook : MonoBehaviour
{
    [SerializeField] private GameObject SplashFX;
    public bool ForceNotSplash = false;
    public void SetSplashFX(bool value) { SplashFX.SetActive(value && !ForceNotSplash); }
}
