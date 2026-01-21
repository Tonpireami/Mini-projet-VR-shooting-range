using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [Header("Targets")]
    public int maxTargets = 3;
    public int currentTargets = 0;

    [Header("Score")]
    public int currentScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    // --- SCORE ---
    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public int GetScore()
    {
        return currentScore;
    }

    // --- TARGET COUNT ---
    public void RegisterTargetSpawn()
    {
        currentTargets++;
    }

    public void RegisterTargetDespawn()
    {
        currentTargets--;
        if (currentTargets < 0)
            currentTargets = 0;
    }
}
