using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private readonly Vector3 _gizmoSize = new Vector3(0.5f, 0.5f, 0.5f);
    
    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            //Gizmos.color = Color.cyan;
            //Gizmos.DrawCube(transform.position, _gizmoSize);
        }
    }
}
