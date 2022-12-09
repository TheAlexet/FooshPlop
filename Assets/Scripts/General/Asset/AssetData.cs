using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AssetData
{
    [field: SerializeField] public List<GameObject> Rods { get; private set; }
}
