using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoLib : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 startPoint = new Vector3(0, 0, 0);
        Vector3 endPoint = new Vector3(1, 1, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, endPoint);

    }
}
