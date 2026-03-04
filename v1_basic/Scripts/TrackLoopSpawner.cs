using System.Collections.Generic;
using UnityEngine;

public class TrackLoopSpawner : MonoBehaviour
{
    [SerializeField] private List<TrackSegment> segmentPrefabs = new();
    [SerializeField] private int initialCount = 3;
    [SerializeField] private Transform player;
    [SerializeField] private float recycleBuffer = 3f;

    private readonly List<TrackSegment> activeSegments = new();
    private float nextSpawnZ;
    private int recycleIndex;

    private void Start()
    {
        for (int i = 0; i < initialCount && segmentPrefabs.Count > 0; i++)
        {
            SpawnSegment(segmentPrefabs[i % segmentPrefabs.Count]);
        }
    }

    private void Update()
    {
        if (player == null || activeSegments.Count == 0)
        {
            return;
        }

        TrackSegment current = activeSegments[recycleIndex];
        float exitZ = current.ExitPoint != null ? current.ExitPoint.position.z : current.transform.position.z + current.Length;
        if (player.position.z < exitZ + recycleBuffer)
        {
            return;
        }

        RecycleCurrentSegment();
    }

    private void SpawnSegment(TrackSegment prefab)
    {
        TrackSegment instance = Instantiate(prefab, new Vector3(0f, 0f, nextSpawnZ), Quaternion.identity, transform);
        activeSegments.Add(instance);
        nextSpawnZ += instance.Length;
    }

    private void RecycleCurrentSegment()
    {
        TrackSegment segment = activeSegments[recycleIndex];
        segment.transform.position = new Vector3(0f, 0f, nextSpawnZ);
        nextSpawnZ += segment.Length;
        recycleIndex = (recycleIndex + 1) % activeSegments.Count;
    }
}
