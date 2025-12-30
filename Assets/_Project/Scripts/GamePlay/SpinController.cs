using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private RouletteView rouletteView;
    [SerializeField] private RouletteWheel rouletteWheel;

    private SpinResult currentResult;

    private void OnEnable()
    {
        InputManager.Instance.OnSpin += HandleSpin;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnSpin -= HandleSpin;
    }

    private void HandleSpin()
    {
        rouletteView.PlaySpin(() =>
        {
            WheelResult wheelResult = rouletteWheel.GetResult();

            currentResult = SpinResolver.Resolve(wheelResult);

            Debug.Log($"Jugador: {PlayerManager.Instance.CurrentPlayer.Name}");
            Debug.Log(currentResult.Type == WheelType.Truth ? "VERDAD" : "RETO");
            Debug.Log($"Dificultad: {currentResult.Difficulty}");
            Debug.Log(currentResult.Card.description);
        });
    }
}
