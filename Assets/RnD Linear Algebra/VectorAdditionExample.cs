using UnityEngine;

public class VectorAdditionExample : MonoBehaviour
{
    public Vector3 a = new Vector3(1, 0, 0);
    public Vector3 b = new Vector3(0, 1, 0);

    private void OnDrawGizmos()
    {
        Vector3 sum = a + b;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(Vector3.zero, a);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(a, sum);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, sum);

    }
}
