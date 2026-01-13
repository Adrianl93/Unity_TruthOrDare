using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeUIController : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text challengeTypeText;
    [SerializeField] private TMP_Text difficultyText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Buttons")]
    [SerializeField] private Button successButton;
    [SerializeField] private Button failButton;

    private GameFlowController gameFlow;

    private void Awake()
    {
        gameFlow = FindObjectOfType<GameFlowController>();

        if (gameFlow == null)
            Debug.LogError("ChallengeUIController: GameFlowController no encontrado");

        successButton.onClick.AddListener(() => gameFlow.CompleteTurn(true));
        failButton.onClick.AddListener(() => gameFlow.CompleteTurn(false));

        gameObject.SetActive(false);
    }

    // ---------------------------------
    // PUBLIC API
    // ---------------------------------

    public void Show(PlayerData player, WheelResult result, ChallengeCard challenge)
    {
        playerNameText.text = $"Turno de: {player.Name}";
        scoreText.text = $"Score: {player.Score}";
        challengeTypeText.text = result.Type.ToString();
        difficultyText.text = result.Difficulty.ToString();
        descriptionText.text = challenge.description;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
