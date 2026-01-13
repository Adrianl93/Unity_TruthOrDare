using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public IReadOnlyList<PlayerData> Players => players;
    public PlayerData CurrentPlayer => players.Count > 0 ? players[currentPlayerIndex] : null;
    public int CurrentPlayerIndex => currentPlayerIndex;

    private readonly List<PlayerData> players = new();
    private int currentPlayerIndex;

    public event Action<PlayerData> OnScoreChanged;
    public event Action<PlayerData> OnTurnChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ---------------------------------
    // INITIALIZATION (FROM SESSION)
    // ---------------------------------

    public void InitializeFromSession()
    {
        ResetManager();

        var session = GameSession.Instance;
        if (session == null || session.Data == null)
        {
            Debug.LogError("PlayerManager: GameSession no inicializada");
            return;
        }

        foreach (var name in session.Data.PlayerNames)
        {
            players.Add(new PlayerData(name));
        }

        currentPlayerIndex = 0;
        OnTurnChanged?.Invoke(CurrentPlayer);

        Debug.Log($"PlayerManager inicializado con {players.Count} jugadores");
    }

    // ---------------------------------
    // TURN MANAGEMENT
    // ---------------------------------

    public void NextTurn()
    {
        if (players.Count == 0)
            return;

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        OnTurnChanged?.Invoke(CurrentPlayer);
    }

    // ---------------------------------
    // SCORE MANAGEMENT
    // ---------------------------------

    public void AddScoreToCurrentPlayer(int amount)
    {
        if (CurrentPlayer == null)
            return;

        CurrentPlayer.AddScore(amount);
        OnScoreChanged?.Invoke(CurrentPlayer);
    }

    // ---------------------------------
    // RESET
    // ---------------------------------

    public void ResetManager()
    {
        players.Clear();
        currentPlayerIndex = 0;

        // IMPORTANTE: limpiamos eventos colgantes
        OnScoreChanged = null;
        OnTurnChanged = null;

        Debug.Log("PlayerManager: reset completo");
    }
}
