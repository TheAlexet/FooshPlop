using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AreaPassSO", menuName = "Menu/Assets/AreaPassSO")]
public class AreaPassSO : ScriptableObject
{
    [field: SerializeField] public string passName { get; private set; }
    [field: SerializeField] public float validityTime { get; private set; } // in seconds
    [field: SerializeField] public List<int> areasUnlocked { get; private set; }
    [field: SerializeField] public float rarity { get; private set; }
}
