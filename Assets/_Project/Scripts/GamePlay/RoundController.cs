using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    private WheelResult currentWheelResult;
    private ChallengeCard currentChallenge;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartRound()
    {
        Debug.Log("Round started");

        // 1. Espera spin
    }

    public void ResolveSpin(WheelResult result)
    {
        currentWheelResult = result;

        currentChallenge = ChallengeManager.Instance.GetRandomChallenge(
            result.Type,
            result.Difficulty
        );

        Debug.Log($"Resultado: {result.Type} | {result.Difficulty}");
        Debug.Log($"Desafío: {currentChallenge.description}");
    }

    public void CompleteRound(bool success)
    {
        if (success)
            PlayerManager.Instance.AddScoreToCurrentPlayer(currentChallenge.scoreSuccess);
        else
            PlayerManager.Instance.AddScoreToCurrentPlayer(currentChallenge.scoreFail);

        PlayerManager.Instance.NextTurn();
    }
}
