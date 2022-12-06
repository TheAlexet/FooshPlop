using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PolygonArea : MonoBehaviour
{
    public List<Transform> vertices;
    List<Vector3> verticesPositions;
    List<Vector3[]> triangles;
    [SerializeField] List<float> areas;

    public bool sampleOnGizmos = false;

    void Start()
    {
        verticesPositions = new List<Vector3>(); // ensures verticesPositions is empty
        foreach (Transform v in vertices) { verticesPositions.Add(v.position); }
        triangles = SplitPolygon();
        areas = new List<float>(); // ensures areas is empty      
        foreach (Vector3[] t in triangles) { areas.Add(GetTriangleArea(t)); }
    }

    public Vector3 RandomPoint()
    {
        int ind = Categorical.Choice(areas);
        Vector3[] triangle = triangles[ind];

        // Sample point inside triangle 
        // (https://blogs.sas.com/content/iml/2020/10/19/random-points-in-triangle.html)
        Vector3 a = triangle[0] - triangle[1];
        Vector3 b = triangle[2] - triangle[1];
        float u1 = Random.Range(0f, 1f);
        float u2 = Random.Range(0f, 1f);
        if (u1 + u2 > 1)
        {
            u1 = 1 - u1;
            u2 = 1 - u2;
        }
        Vector3 w = u1 * a + u2 * b;
        return w + triangle[1];
    }

    void OnDrawGizmosSelected()
    {
        if (sampleOnGizmos)
        {
            verticesPositions = new List<Vector3>(); // ensures verticesPositions is empty
            foreach (Transform v in vertices) { verticesPositions.Add(v.position); }
            triangles = SplitPolygon();
            areas = new List<float>(); // ensures areas is empty      
            foreach (Vector3[] t in triangles) { areas.Add(GetTriangleArea(t)); }
            Gizmos.DrawSphere(RandomPoint(), 1f);
        }
    }

    List<Vector3[]> SplitPolygon()
    {
        List<Vector3[]> triangles = new List<Vector3[]>(); // ensure list is empty
        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(new Vector3[] { verticesPositions[0], verticesPositions[i], verticesPositions[i + 1] });
        }
        return triangles;
    }

    float GetTriangleArea(Vector3[] triangle)
    {
        return (triangle[0] - triangle[1]).magnitude * (triangle[0] - triangle[2]).magnitude / 2;
    }

}
