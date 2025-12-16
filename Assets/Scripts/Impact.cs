using UnityEngine;

public class Impact : MonoBehaviour
{
    public GameObject impactPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cible"))
        {
            Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
