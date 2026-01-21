using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fusil : MonoBehaviour
{
    public Transform muzzle;
    public float bulletSpeed = 20f;
    public float fireRate = 0.3f;

    public AudioClip shootSound;
    private AudioSource audioSource;

    private float lastFireTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(ActivateEventArgs args)
    {
        if (Time.time - lastFireTime < fireRate)
            return;

        lastFireTime = Time.time;

        GameObject muzzleFX = FXManager.Instance.GetMuzzleFX();
        if (muzzleFX != null)
        {
            GameObject fx = Instantiate(muzzleFX, muzzle.position, muzzle.rotation);

            float maxDuration = 0f;
            foreach (var ps in fx.GetComponentsInChildren<ParticleSystem>())
            {
                if (ps.main.duration > maxDuration)
                    maxDuration = ps.main.duration;
            }
        }

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet = ObjectPool.Instance.SpawnFromPool(
            "Projectile",
            muzzle.position,
            muzzle.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = muzzle.forward * bulletSpeed;
    }
}
