using UnityEngine;
using System.Collections;

public class SpecialEventManager : MonoBehaviour
{
    public static SpecialEventManager Instance;

    public GameObject Music;
    public GameObject EasterEgg;
    public GameObject Danseurs;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerSpecialSequence()
    {
        StartCoroutine(SpecialSequence());
    }

    private IEnumerator SpecialSequence()
    {
        if (Music != null) Music.SetActive(false);
        if (EasterEgg != null) EasterEgg.SetActive(true);

        yield return new WaitForSeconds(21);

        if (Danseurs != null) Danseurs.SetActive(true);

        yield return new WaitForSeconds(36);

        Destroy(Danseurs);
        Destroy(EasterEgg);
        if (Music != null) Music.SetActive(true);
    }
}