using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    public static ProjectilePoolManager Instance;

    public float maxDistance = 50f;
    public float checkInterval = 0.3f;

    private List<Projectile> activeProjectiles = new List<Projectile>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(CleanupRoutine());
    }

    public void RegisterProjectile(Projectile p)
    {
        activeProjectiles.Add(p);
    }

    public void UnregisterProjectile(Projectile p)
    {
        activeProjectiles.Remove(p);
    }

    private IEnumerator CleanupRoutine()
    {
        while (true)
        {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = activeProjectiles[i];

                if (!p.gameObject.activeSelf)
                    continue;

                float dist = Vector3.Distance(p.startPosition, p.transform.position);

                if (dist > maxDistance)
                {
                    p.DisableProjectile(); 
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
