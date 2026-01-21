using UnityEngine;
using System.Collections;

public class AutoDisableFX : MonoBehaviour
{
    public float lifetime = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
