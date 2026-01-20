using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject explosionPrefab;

    public Vector3 explosionOffset = new Vector3(0, 1f, 0);

    private int spawnIndex;
    private TargetSpawner spawner;

    public void Initialize(TargetSpawner spawnerRef, int index)
    {
        explosionPrefab = FXManager.Instance.GetExplosionFX();
        spawner = spawnerRef;
        spawnIndex = index;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SpawnExplosion();
            Disable();
        }
    }

    void SpawnExplosion()
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

        Destroy(explosion, maxDuration + 0.5f);
    }

    public void Disable()
    {
        spawner.FreeSpawnPoint(spawnIndex);
        gameObject.SetActive(false);
        spawner.SpawnOneTarget();
    }
}
