using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNavigationController : MonoBehaviour
{
    [Header("Scene Names")]
   
    [SerializeField] private string playerSetupScene = "PlayerSetup";
    [SerializeField] private string scoreboardScene = "Scoreboard";

    // ---------------------------------
    // UI BUTTON CALLBACKS
    // ---------------------------------

    /// <summary>
    /// Vuelve a la escena de setup de jugadores.
    /// NO reinicia la GameSession.
    /// </summary>
    public void ReturnToPlayerSetup()
    {
        Debug.Log("Navigation: Volviendo a PlayerSetup");
        SceneManager.LoadScene(playerSetupScene);
    }

    /// <summary>
    /// Finaliza la partida y muestra el Scoreboard.
    /// NO reinicia la GameSession.
    /// </summary>
    public void GoToScoreboard()
    {
        Debug.Log("Navigation: Game Over => Scoreboard");
        SceneManager.LoadScene(scoreboardScene);
    }
}
