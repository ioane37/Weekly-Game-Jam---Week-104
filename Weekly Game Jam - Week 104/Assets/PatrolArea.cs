using UnityEngine;

public class PatrolArea : MonoBehaviour
{
    [SerializeField] float pointRadius = 0.5f;

    private int GetNextIndex(int i)
    {
        if (i + 1 == transform.childCount)
            return 0;

        return i + 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, pointRadius);
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(GetNextIndex(i)).position);
        }
    }
}