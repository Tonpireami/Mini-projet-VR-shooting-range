using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private float lifeTimer = 0f;
    public float maxLifeTime = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        lifeTimer = 0f;
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLifeTime)
        {
            Disable();
        }
    }

    public void Disable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
