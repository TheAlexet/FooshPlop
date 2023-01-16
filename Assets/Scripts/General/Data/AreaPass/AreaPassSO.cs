using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AreaPassSO", menuName = "Menu/Assets/AreaPassSO")]
public class AreaPassSO : ScriptableObject
{
    [field: SerializeField] public string PassName { get; private set; }
    [field: SerializeField] public float ValidityTime { get; private set; } // in seconds
    [field: SerializeField] public List<int> AreasUnlocked { get; private set; }
    [field: SerializeField] public float SpawnRate { get; private set; }
}
