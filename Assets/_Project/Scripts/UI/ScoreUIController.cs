using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (PlayerManager.Instance == null)
            return;

        PlayerManager.Instance.OnScoreChanged += UpdateScore;
        PlayerManager.Instance.OnTurnChanged += UpdateTurn;
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance == null)
            return;

        PlayerManager.Instance.OnScoreChanged -= UpdateScore;
        PlayerManager.Instance.OnTurnChanged -= UpdateTurn;
    }


    private void UpdateScore(PlayerData player)
    {
        scoreText.text = player.Score.ToString();
    }

    private void UpdateTurn(PlayerData player)
    {
        playerName.text = player.Name;
        scoreText.text = player.Score.ToString();
    }
}
