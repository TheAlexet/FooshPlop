using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CustomizationData
{
    [field: SerializeField] public List<GameObject> Rods { get; private set; }
    [field: SerializeField] public List<GameObject> Hats { get; private set; }
    [field: SerializeField] public List<Material> Slips { get; private set; }
    [field: SerializeField] public Material Skin { get; private set; }

}
