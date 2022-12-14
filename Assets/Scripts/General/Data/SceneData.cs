using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;
using System;

[Serializable]
public class SceneData
{
    [field: SerializeField][field: Range(0f, 2f)] public float FishScale { get; private set; }
    [field: SerializeField][field: MinMaxSlider(0f, 6f)] public Vector2 SpawnDelay { get; private set; }
    [field: SerializeField] public List<GameObject> FishesInScene { get; private set; }
}
