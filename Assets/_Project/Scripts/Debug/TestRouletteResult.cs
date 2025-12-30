using UnityEngine;

public class TestRouletteResult : MonoBehaviour
{
    [SerializeField] private RouletteWheel roulette;

    public void TestResult()
    {
        WheelResult result = roulette.GetResult();
        Debug.Log($"RESULTADO => {result.Type} | {result.Difficulty}");
    }
}
