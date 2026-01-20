using UnityEngine;

public class Impact : MonoBehaviour
{
    private GameObject impactPrefab;
    private Projectile projectile;

    private void Awake()
    {
        projectile = GetComponent<Projectile>();
        impactPrefab = FXManager.Instance.GetImpactFX();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cible"))
        {
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

                Destroy(impact, maxDuration + 0.5f);
            }

            projectile.Disable();
        }

        // 🎯 ICI : la cible spéciale
        if (collision.gameObject.CompareTag("Speciale"))
        {
    #if UNITY_ANDROID
            return; // Sur Quest : on ignore l'easter egg
    #endif

            // Détruire la cible spéciale
            Destroy(collision.gameObject);

            // Désactiver le projectile
            projectile.Disable();

            // 🎉 Lancer la séquence spéciale
            SpecialEventManager.Instance.TriggerSpecialSequence();
        }
    }
}