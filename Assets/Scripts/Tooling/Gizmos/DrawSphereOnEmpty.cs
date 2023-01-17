using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphereOnEmpty : MonoBehaviour
{

    public float radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
