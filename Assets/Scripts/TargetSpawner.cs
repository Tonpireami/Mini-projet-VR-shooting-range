using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public string[] robotBaseTags;

    public float spawnInterval = 1.5f;

    private string[] platformTags;
    private bool[] spawnOccupied;
    private int lastSpawnIndex = -1;

    private void Start()
    {
        spawnOccupied = new bool[spawnPoints.Length];

#if UNITY_ANDROID
        platformTags = new string[robotBaseTags.Length];
        for (int i = 0; i < robotBaseTags.Length; i++)
            platformTags[i] = robotBaseTags[i] + "_Quest";
#else
        platformTags = robotBaseTags;
#endif

        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameplayManager.Instance.currentTargets < GameplayManager.Instance.maxTargets)
            {
                int index = GetFreeSpawnPoint();
                if (index != -1)
                    SpawnOneTargetAt(index);
            }
        }
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

    private void SpawnOneTargetAt(int index)
    {
        string tag = platformTags[Random.Range(0, platformTags.Length)];

        GameObject target = ObjectPool.Instance.SpawnFromPool(
            tag,
            spawnPoints[index].position,
            spawnPoints[index].rotation
        );

        Target t = target.GetComponent<Target>();
        t.Initialize(this, index);

        spawnOccupied[index] = true;
        GameplayManager.Instance.RegisterTargetSpawn();
    }

    public void FreeSpawnPoint(int index)
    {
        spawnOccupied[index] = false;
    }
}
