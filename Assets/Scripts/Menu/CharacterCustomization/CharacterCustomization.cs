using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [field: SerializeField] public AssetScriptableObject AssetSO { get; private set; }
    [field: SerializeField] private Transform PlayerHand;

    private List<GameObject> Rods;
    private int currentRodIndex;
    private GameObject currentRod;

    private void Awake()
    {
        Rods = AssetSO.AssetData.Rods;
        SpawnRod();
        // currentRod = PlayerRef.Get(currentRod) smthg like that
    }

    public void SpawnRod()
    {
        currentRod = GameObject.Instantiate(Rods[currentRodIndex], PlayerHand);
    }

    public void NextRod()
    {
        Destroy(currentRod);
        currentRodIndex = (currentRodIndex + 1) % Rods.Count;
        SpawnRod();
    }
}
