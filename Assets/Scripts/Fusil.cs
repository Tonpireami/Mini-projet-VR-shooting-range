using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fusil : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.3f;

    [Header("Audio")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    float lastFireTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(ActivateEventArgs args)
    {
        if (Time.time - lastFireTime < fireRate)
            return;

        lastFireTime = Time.time;

        muzzleFlash.Play();

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = muzzle.forward * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
