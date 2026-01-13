using TMPro;
using UnityEngine;

public class CurrentPlayerUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogWarning("CurrentPlayerUIController: PlayerManager aún no disponible");
            return;
        }

        var player = PlayerManager.Instance.CurrentPlayer;

        if (player == null)
            return;

        playerNameText.text = $"Turno de: {player.Name}";
        scoreText.text = $"Score: {player.Score}";
    }
}
