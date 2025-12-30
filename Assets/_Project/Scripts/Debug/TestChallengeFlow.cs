using UnityEngine;

public class TestChallengeFlow : MonoBehaviour
{
    private void Start()
    {
        var challengeManager = FindObjectOfType<ChallengeManager>();

        challengeManager.LoadCategory("Party");

        var card = challengeManager.GetRandomChallenge(
            WheelType.Truth,
            difficulty: Difficulty.Easy
        );

        if (card != null)
        {
            Debug.Log($"DESAFÍO: {card.description}");
            Debug.Log($"OK: +{card.scoreSuccess} | FAIL: {card.scoreFail}");
        }
        else
        {
            Debug.LogError("No se encontró desafío");
        }
    }
}
