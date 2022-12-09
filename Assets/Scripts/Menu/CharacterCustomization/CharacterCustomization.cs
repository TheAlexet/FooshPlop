using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [field: SerializeField] public AssetScriptableObject AssetSO { get; private set; }
    [field: SerializeField] private Transform PlayerHand;
    [field: SerializeField] private Transform PlayerHead;

    private List<GameObject> Rods;
    private int currentRodIndex;
    private GameObject currentRod;

    private List<GameObject> Hats;
    private int currentHatIndex;
    private GameObject currentHat;

    private void Awake()
    {
        Rods = AssetSO.AssetData.Rods;
        Hats = AssetSO.AssetData.Hats;

        SpawnRod();
        SpawnHat();
        // currentRod = PlayerRef.Get(currentRod) smthg like that
    }

    public void SpawnRod()
    {
        currentRod = GameObject.Instantiate(Rods[currentRodIndex], PlayerHand);
    }
    public void SpawnHat()
    {
        currentHat = GameObject.Instantiate(Hats[currentHatIndex], PlayerHead);
    }

    public void NextRod()
    {
        Destroy(currentRod);
        currentRodIndex = (currentRodIndex + 1) % Rods.Count;
        SpawnRod();
    }
    public void NextHat()
    {
        Destroy(currentHat);
        currentHatIndex = (currentHatIndex + 1) % Hats.Count;
        SpawnHat();
    }
}
