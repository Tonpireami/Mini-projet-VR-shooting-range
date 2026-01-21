using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
        ProjectilePoolManager.Instance.RegisterProjectile(this);
    }

    public void DisableProjectile()
    {
        gameObject.SetActive(false);
        ProjectilePoolManager.Instance.UnregisterProjectile(this);
    }
}
