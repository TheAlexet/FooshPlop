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
        int initialRodIndex = currentRodIndex;
        for(currentRodIndex = (currentRodIndex + 1) % Rods.Count; currentRodIndex != initialRodIndex; currentRodIndex = (currentRodIndex + 1) % Rods.Count)
        {
            if(currentRodIndex == 0 || Database.getPurchasedItem(Rods[currentRodIndex].transform.GetComponent<Item>().Data.Name) == true)
            {
                Destroy(currentRod);
                Database.SetCurrentRodIndex(currentRodIndex);
                SpawnRod();
                buttonSound.Play();
                break;
            }
        }
    }
    public void NextHat()
    {
        int initialHatIndex = currentHatIndex;
        for(currentHatIndex = (currentHatIndex + 1) % Hats.Count; currentHatIndex != initialHatIndex; currentHatIndex = (currentHatIndex + 1) % Hats.Count)
        {
            if(currentHatIndex == 0 || Database.getPurchasedItem(Hats[currentHatIndex].transform.GetComponent<Item>().Data.Name) == true)
            {
                Destroy(currentHat);
                Database.SetCurrentHatIndex(currentHatIndex);
                SpawnHat();
                buttonSound.Play();
                break;
            }
        }
    }
    public void NextSlip()
    {
        int initialSlipIndex = currentSlipIndex;
        for(currentSlipIndex = (currentSlipIndex + 1) % Slips.Count; currentSlipIndex != initialSlipIndex; currentSlipIndex = (currentSlipIndex + 1) % Slips.Count)
        {
            if(currentSlipIndex == 0 || Database.getPurchasedItem(Slips[currentSlipIndex].name) == true)
            {      
                Database.SetCurrentSlipIndex(currentSlipIndex);
                ApplySlip();
                buttonSound.Play();
                break;
            }
        }

    }

    private Material[] MatArray()
    {
        return new Material[2]{
            CustomizationSO.CustomizationData.Skin, Slips[currentSlipIndex]
        };
    }
}
