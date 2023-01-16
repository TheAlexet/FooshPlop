using System;
using UnityEngine;
using GD.MinMaxSlider;

[Serializable]
public class FishData
{
    [field: Header("Fish Identity")]
    [field: SerializeField] public string FishName { get; private set; }
    [field: SerializeField] public int Rarity { get; private set; }
    [field: SerializeField] public float SpawnRate { get; private set; }
    [field: SerializeField] public string FancyName { get; private set; }

    [field: Header("Fish In Game Properties")]
    [field: SerializeField][field: MinMaxSlider(0f, 10f)] public Vector2 ChangeDestinationDelay { get; private set; }
    [field: SerializeField][field: MinMaxSlider(0f, 10f)] public Vector2 CatchBeforeDelay { get; private set; }
    [field: SerializeField] public float TranslateSpeed { get; private set; }
    [field: SerializeField] public float RotateSpeed { get; private set; }

    [field: Header("Fish in Pokedex")]
    [field: SerializeField] public Vector3 Offset { get; private set; }
}
