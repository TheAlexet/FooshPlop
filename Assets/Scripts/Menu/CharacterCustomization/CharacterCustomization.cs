using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [field: SerializeField] public AssetScriptableObject AssetSO { get; private set; }
    [field: SerializeField] private Transform PlayerHand;
    [field: SerializeField] private Transform PlayerHead;

    private DatabaseAccess Database;

    private List<GameObject> Rods;
    private int currentRodIndex;
    private GameObject currentRod;

    private List<GameObject> Hats;
    private int currentHatIndex;
    private GameObject currentHat;

    private void Awake()
    {
        Database = new DatabaseAccess();

        Rods = AssetSO.AssetData.Rods;
        Hats = AssetSO.AssetData.Hats;

        currentRodIndex = Database.GetCurrentRodIndex();
        currentHatIndex = Database.GetCurrentHatIndex();
        SpawnRod();
        SpawnHat();
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
        Database.SetCurrentRodIndex(currentRodIndex);
        SpawnRod();
    }
    public void NextHat()
    {
        Destroy(currentHat);
        currentHatIndex = (currentHatIndex + 1) % Hats.Count;
        Database.SetCurrentHatIndex(currentHatIndex);
        SpawnHat();
    }
}
