using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetSO", menuName = "Menu/Assets/AssetSO")]
public class AssetScriptableObject : ScriptableObject
{
    [field: SerializeField] public AssetData AssetData { get; private set; }
}
