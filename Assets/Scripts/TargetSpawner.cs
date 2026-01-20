using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    private bool[] spawnOccupied;

    [Header("Robot Base Tags")]
    public string[] robotBaseTags; // ex: "Guardian", "Invader", "Scout"

    [Header("Settings")]
    public int maxTargets = 3;

    private int activeTargets = 0;
    private int lastSpawnIndex = -1;

    // Liste des tags adaptés à la plateforme (ex: "Guardian_Quest" ou "Guardian")
    private string[] platformTags;

    private void Start()
    {
        spawnOccupied = new bool[spawnPoints.Length];

        // Adapter les tags selon la plateforme
#if UNITY_ANDROID
        platformTags = new string[robotBaseTags.Length];
        for (int i = 0; i < robotBaseTags.Length; i++)
            platformTags[i] = robotBaseTags[i] + "_Quest";
#else
        platformTags = robotBaseTags;
#endif

        // Spawn initial
        for (int i = 0; i < maxTargets; i++)
            SpawnOneTarget();
    }

    public void SpawnOneTarget()
    {
        if (activeTargets >= maxTargets)
            return;

        int spawnIndex = GetFreeSpawnPoint();
        if (spawnIndex == -1)
            return;

        string tag = platformTags[Random.Range(0, platformTags.Length)];

        GameObject target = ObjectPool.Instance.SpawnFromPool(
            tag,
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        Target t = target.GetComponent<Target>();
        t.Initialize(this, spawnIndex);

        spawnOccupied[spawnIndex] = true;
        activeTargets++;
    }

    public void FreeSpawnPoint(int index)
    {
        spawnOccupied[index] = false;
        activeTargets--;
    }

    private int GetFreeSpawnPoint()
    {
        List<int> free = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnOccupied[i])
                free.Add(i);
        }

        if (free.Count == 0)
            return -1;

        if (free.Count > 1 && free.Contains(lastSpawnIndex))
            free.Remove(lastSpawnIndex);

        int chosen = free[Random.Range(0, free.Count)];
        lastSpawnIndex = chosen;

        return chosen;
    }
}
