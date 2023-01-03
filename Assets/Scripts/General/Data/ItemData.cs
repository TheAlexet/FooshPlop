using System;
using UnityEngine;
using GD.MinMaxSlider;

[Serializable]
public class ItemData
{
    [field: Header("Item Identity")]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Acorns { get; private set; }
    [field: SerializeField] public float ShopSize { get; private set; }
}