using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ChallengeUIController challengeUI;
    [SerializeField] private CurrentPlayerUIController currentPlayerUI;

    private SpinResult currentSpin;
    public bool CanSpin { get; private set; }

    private void Awake()
    {
        ValidateReferences();
    }

    private void OnEnable()
    {
        ResetFlow();
    }

    private void ValidateReferences()
    {
        if (challengeUI == null)
            Debug.LogError("GameFlowController: ChallengeUIController NO asignado");

        if (currentPlayerUI == null)
            Debug.LogError("GameFlowController: CurrentPlayerUIController NO asignado");
    }

    // =========================
    // SPIN FLOW
    // =========================

    public bool RequestSpin()
    {
        Debug.Log($"RequestSpin | CanSpin={CanSpin}");

        if (!CanSpin)
        {
            Debug.Log("GameFlowController: Spin bloqueado");
            return false;
        }

        CanSpin = false;
        return true;
    }

    public void ShowChallenge(SpinResult spinResult)
    {
        if (spinResult.Card == null)
        {
            Debug.LogWarning("GameFlowController: Spin sin carta, liberando spin");
            CanSpin = true;
            return;
        }

        currentSpin = spinResult;

        currentPlayerUI?.Refresh();

        if (challengeUI == null)
        {
            Debug.LogError("GameFlowController: ChallengeUI NULL en ShowChallenge");
            CanSpin = true;
            return;
        }

        challengeUI.Show(
            PlayerManager.Instance.CurrentPlayer,
            new WheelResult(spinResult.Type, spinResult.Difficulty),
            spinResult.Card
        );
    }

    public void CompleteTurn(bool success)
    {
        if (currentSpin.Card == null)
        {
            Debug.LogWarning("GameFlowController: CompleteTurn llamado sin spin activo");
            return;
        }

        int score = success
            ? currentSpin.Card.scoreSuccess
            : currentSpin.Card.scoreFail;

        PlayerManager.Instance.AddScoreToCurrentPlayer(score);
        PlayerManager.Instance.NextTurn();

        currentSpin = default;

        challengeUI.Hide();
        currentPlayerUI?.Refresh();

        CanSpin = true;

        Debug.Log("GameFlowController: Turno completado, spin habilitado");
    }

    // =========================
    // RESET
    // =========================

    public void ResetFlow()
    {
        currentSpin = default;
        CanSpin = true;

        if (challengeUI != null)
            challengeUI.Hide();

        Debug.Log("GameFlowController: Flow reseteado completamente");
    }
}
