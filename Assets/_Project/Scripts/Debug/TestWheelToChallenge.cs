using UnityEngine;

public class TestWheelToChallenge : MonoBehaviour
{
    [SerializeField] private RouletteWheel roulette;
    [SerializeField] private string selectedCategory;

    private void Start()
    {
        ChallengeManager.Instance.LoadCategory(selectedCategory);
    }

    public void TestFlow()
    {
        WheelResult result = roulette.GetResult();

        ChallengeCard challenge = ChallengeManager.Instance.GetRandomChallenge(
            result.Type,
            result.Difficulty
        );

        if (challenge != null)
        {
            Debug.Log("DESAFÍO:");
            Debug.Log(challenge.description);
            Debug.Log($"OK: +{challenge.scoreSuccess} | FAIL: {challenge.scoreFail}");
        }
        else
        {
            Debug.LogWarning("No se encontró desafío para este resultado");
        }
    }
}
