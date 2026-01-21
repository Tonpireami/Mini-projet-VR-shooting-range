using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    private GameObject explosionPrefab;

    public Vector3 explosionOffset = new Vector3(0, 1f, 0);

    private int spawnIndex;
    private TargetSpawner spawner;

    public void Initialize(TargetSpawner spawnerRef, int index)
    {
        spawner = spawnerRef;
        spawnIndex = index;

        explosionPrefab = FXManager.Instance.GetExplosionFX();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SpawnExplosion();
            GameplayManager.Instance.AddScore(5);
            Disable();
        }
    }

    private void SpawnExplosion()
    {
        if (explosionPrefab == null)
            return;

        GameObject explosion = Instantiate(
            explosionPrefab,
            transform.position + explosionOffset,
            Quaternion.identity
        );

        float maxDuration = 0f;
        foreach (var ps in explosion.GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.main.duration > maxDuration)
                maxDuration = ps.main.duration;
        }
    }

    public void Disable()
    {
        spawner.FreeSpawnPoint(spawnIndex);
        GameplayManager.Instance.RegisterTargetDespawn();
        StartCoroutine(DisableAfterFrame());
    }

    private IEnumerator DisableAfterFrame()
    {
        yield return null;
        gameObject.SetActive(false);
    }
}
