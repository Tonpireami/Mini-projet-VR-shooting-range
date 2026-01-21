using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        StartCoroutine(UpdateScoreRoutine());
    }

    private IEnumerator UpdateScoreRoutine()
    {
        while (true)
        {
            scoreText.text = "Score : " + GameplayManager.Instance.GetScore();
            yield return new WaitForSeconds(0.15f);
        }
    }
}
