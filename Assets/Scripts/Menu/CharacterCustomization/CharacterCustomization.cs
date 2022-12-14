using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [field: SerializeField] public CustomizationScriptableObject CustomizationSO { get; private set; }
    [field: SerializeField] private Transform PlayerHand;
    [field: SerializeField] private Transform PlayerHead;
    [field: SerializeField] private SkinnedMeshRenderer SlipMesh;

    [SerializeField] private AudioSource buttonSound;


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

        Rods = CustomizationSO.CustomizationData.Rods;
        Hats = CustomizationSO.CustomizationData.Hats;
        Slips = CustomizationSO.CustomizationData.Slips;

        currentRodIndex = Database.GetCurrentRodIndex();
        currentHatIndex = Database.GetCurrentHatIndex();
        currentSlipIndex = Database.GetCurrentSlipIndex();
        SpawnRod();
        SpawnHat();
        ApplySlip();
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
        buttonSound.Play();
    }
    public void NextHat()
    {
        Destroy(currentHat);
        currentHatIndex = (currentHatIndex + 1) % Hats.Count;
        Database.SetCurrentHatIndex(currentHatIndex);
        SpawnHat();
        buttonSound.Play();
    }
    public void NextSlip()
    {
        currentSlipIndex = (currentSlipIndex + 1) % Slips.Count;
        Database.SetCurrentSlipIndex(currentSlipIndex);
        ApplySlip();
        buttonSound.Play();
    }

    private Material[] MatArray()
    {
        return new Material[2]{
            CustomizationSO.CustomizationData.Skin, Slips[currentSlipIndex]
        };
    }
}
