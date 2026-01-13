using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour
{
    [SerializeField] private CurrentPlayerUIController currentPlayerUI;
    [SerializeField] private GameFlowController gameFlow;
    [SerializeField] private RouletteView rouletteView;
    private void Start()
    {
        rouletteView.ForceReset();

      
        var session = GameSession.Instance;

        if (session == null || session.Data == null)
        {
            Debug.LogError("GameSession no inicializada. Volviendo al menú.");
            return;
        }

       

        // Inicializar jugadores desde sesión
        PlayerManager.Instance.InitializeFromSession();

        // LOG DE JUGADORES (debug de flujo)
        LogPlayers();

        // Inicializar desafíos desde sesión
        ChallengeManager.Instance.InitializeFromSession();

        currentPlayerUI.Refresh();

        gameFlow.ResetFlow();

        // Estado del juego
        GameManager.Instance.SetState(GameState.Playing);
    }

    private void LogPlayers()
    {
        var players = PlayerManager.Instance.Players;

        Debug.Log($"Jugadores inicializados: {players.Count}");

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log($"Jugador {i + 1}: {players[i].Name}");
        }
    }
}
