using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerSetupNavigationController : MonoBehaviour
{
    [SerializeField] private PlayerSetupController playerSetup;

    public void GoBack()
    {
        SceneManager.LoadScene("CategorySelect");
    }

    public void ConfirmAndStartGame()
    {
        List<string> playerNames = playerSetup.GetPlayerNames();

        if (playerNames == null || playerNames.Count == 0)
        {
            Debug.LogWarning("No hay jugadores válidos");
            return;
        }

        // Guardamos en sesión
        GameSession.Instance.Data.PlayerNames = playerNames;

        // Avanzamos al juego
        SceneManager.LoadScene("Game");
    }
}
