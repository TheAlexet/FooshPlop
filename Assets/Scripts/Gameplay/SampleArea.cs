using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleArea : MonoBehaviour
{
    [SerializeField] Transform corner1;
    [SerializeField] Transform corner2;
    [SerializeField] Transform corner3;
    [SerializeField] Transform corner4;

    public bool drawGizmos = false;

    public Vector3 RandomPoint()
    {
        Vector3 c1 = corner1.position;
        Vector3 c2 = corner2.position;
        Vector3 c3 = corner3.position;
        Vector3 c4 = corner4.position;

        // Create triangles
        Vector3[] triangle1 = new Vector3[] { c1, c2, c3 };
        Vector3[] triangle2 = new Vector3[] { c3, c4, c1 };

        // Choose triangle based on area
        float area_1 = (c1 - c2).magnitude * (c2 - c3).magnitude / 2;
        float area_2 = (c3 - c4).magnitude * (c4 - c1).magnitude / 2;
        float p1 = area_1 / (area_1 + area_2);
        float p2 = area_2 / (area_1 + area_2);
        List<float> probs = new List<float>();
        probs.Add(p1);
        probs.Add(p2);
        int ind = new Categorical().Choice(probs);

        Vector3[] triangle = ind == 0 ? triangle1 : triangle2;

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
        if (drawGizmos) { Gizmos.DrawSphere(RandomPoint(), 1f); }
    }

}
