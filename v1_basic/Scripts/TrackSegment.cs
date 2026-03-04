using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float fallbackLength = 20f;

    public Transform ExitPoint => exitPoint;
    public float Length => exitPoint != null ? exitPoint.localPosition.z : fallbackLength;
}
