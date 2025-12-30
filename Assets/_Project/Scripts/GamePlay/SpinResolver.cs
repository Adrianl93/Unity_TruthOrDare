using UnityEngine;

public static class SpinResolver
{
    public static SpinResult Resolve(WheelResult wheelResult)
    {
        var card = ChallengeManager.Instance.GetRandomChallenge(
            wheelResult.Type,
            wheelResult.Difficulty
        );

        return new SpinResult
        {
            Type = wheelResult.Type,
            Difficulty = wheelResult.Difficulty,
            Card = card
        };
    }
}
