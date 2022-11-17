using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawSpawnArea : MonoBehaviour
{
    [SerializeField] Transform corner1;
    [SerializeField] Transform corner2;
    [SerializeField] Transform corner3;
    [SerializeField] Transform corner4;

    public Color drawColor;
    public float thickness;


    void OnDrawGizmos()
    {
        Handles.DrawBezier(corner1.position, corner2.position, corner1.position, corner2.position, drawColor, null, thickness);
        Handles.DrawBezier(corner2.position, corner3.position, corner2.position, corner3.position, drawColor, null, thickness);
        Handles.DrawBezier(corner3.position, corner4.position, corner3.position, corner4.position, drawColor, null, thickness);
        Handles.DrawBezier(corner4.position, corner1.position, corner4.position, corner1.position, drawColor, null, thickness);
    }
}
