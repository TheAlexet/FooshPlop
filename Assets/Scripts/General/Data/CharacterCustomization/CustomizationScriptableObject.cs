using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomizationSO", menuName = "Menu/Assets/CustomizationSO")]
public class CustomizationScriptableObject : ScriptableObject
{
    [field: SerializeField] public CustomizationData CustomizationData { get; private set; }
}
