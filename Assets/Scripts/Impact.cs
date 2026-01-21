using UnityEngine;

public class Impact : MonoBehaviour
{
    private Projectile projectile;

    private void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cible"))
        {
            GameObject impactPrefab = FXManager.Instance.GetImpactFX();

            if (impactPrefab != null)
            {
                GameObject impact = Instantiate(
                    impactPrefab,
                    collision.contacts[0].point,
                    Quaternion.identity
                );

                float maxDuration = 0f;
                foreach (var ps in impact.GetComponentsInChildren<ParticleSystem>())
                {
                    if (ps.main.duration > maxDuration)
                        maxDuration = ps.main.duration;
                }
            }

            projectile.DisableProjectile();
        }

        if (collision.gameObject.CompareTag("Speciale"))
        {
#if UNITY_ANDROID
            return;
#endif

            Destroy(collision.gameObject);
            projectile.DisableProjectile();
            SpecialEventManager.Instance.TriggerSpecialSequence();
        }
    }
}
