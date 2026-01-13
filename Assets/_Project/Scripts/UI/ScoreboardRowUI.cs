using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text positionText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image crownIcon;

    public void SetData(int position, PlayerData player)
    {
        positionText.text = $"{position}°";
        playerNameText.text = player.Name;
        scoreText.text = $"{player.Score} pts";

        crownIcon.gameObject.SetActive(position == 1);
    }
}
