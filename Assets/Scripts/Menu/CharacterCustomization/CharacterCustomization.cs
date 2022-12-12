using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [field: SerializeField] public AssetScriptableObject AssetSO { get; private set; }
    [field: SerializeField] private Transform PlayerHand;
    [field: SerializeField] private Transform PlayerHead;
    [field: SerializeField] private SkinnedMeshRenderer SlipMesh;


    private DatabaseAccess Database;

    private List<GameObject> Rods;
    private int currentRodIndex;
    private GameObject currentRod;

    private List<GameObject> Hats;
    private int currentHatIndex;
    private GameObject currentHat;

    public List<Material> Slips;
    public int currentSlipIndex;


    private void Awake()
    {
        Database = new DatabaseAccess();

        Rods = AssetSO.AssetData.Rods;
        Hats = AssetSO.AssetData.Hats;
        Slips = AssetSO.AssetData.Slips;
        // Debug.Log(Slips.Count);

        currentRodIndex = Database.GetCurrentRodIndex();
        currentHatIndex = Database.GetCurrentHatIndex();
        currentSlipIndex = Database.GetCurrentSlipIndex();
        SpawnRod();
        SpawnHat();
        ApplySlip();
        // Debug.Log(Slips.Count);

    }

    public void SpawnRod()
    {
        currentRod = GameObject.Instantiate(Rods[currentRodIndex], PlayerHand);
    }
    public void SpawnHat()
    {
        currentHat = GameObject.Instantiate(Hats[currentHatIndex], PlayerHead);
    }

    public void ApplySlip()
    {
        SlipMesh.materials = MatArray();
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
    public void NextSlip()
    {
        Debug.Log(Slips.Count);

        // Destroy(currentSlip);
        currentSlipIndex = (currentSlipIndex + 1) % Slips.Count;
        Database.SetCurrentSlipIndex(currentSlipIndex);
        ApplySlip();
    }

    private Material[] MatArray()
    {
        return new Material[2]{
            AssetSO.AssetData.Skin, Slips[currentSlipIndex]
        };
    }
}
