using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawPolygon : MonoBehaviour
{
    [SerializeField] PolygonArea polygonArea;
    public Color drawColor;
    public float thickness;


    void OnDrawGizmos()
    {
        List<Transform> vertices = polygonArea.vertices;
        for (int i = 0; i <= vertices.Count; i++)
        {
            Vector3 v1 = vertices[i % vertices.Count].position;
            Vector3 v2 = vertices[(i + 1) % vertices.Count].position;
            Handles.DrawBezier(v1, v2, v1, v2, drawColor, null, thickness);
        }
    }
}
