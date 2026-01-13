using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private RouletteView rouletteView;
    [SerializeField] private RouletteWheel rouletteWheel;
    [SerializeField] private GameFlowController gameFlow;

    private void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSpin += HandleSpin;
            Debug.Log("SpinController: Suscripto a OnSpin");
        }
        else
        {
            Debug.LogError("SpinController: InputManager no disponible en Start");
        }
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSpin -= HandleSpin;
            Debug.Log("SpinController: Desuscripto de OnSpin");
        }
    }

    private void HandleSpin()
    {
        Debug.Log("SpinController: HandleSpin EJECUTADO");

        if (!gameFlow.RequestSpin())
            return;

        rouletteView.PlaySpin(() =>
        {
            WheelResult wheelResult = rouletteWheel.GetResult();
            SpinResult spinResult = SpinResolver.Resolve(wheelResult);

            gameFlow.ShowChallenge(spinResult);
        });
    }
}
