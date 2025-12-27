using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public List<PlayerData> Players { get; private set; } = new List<PlayerData>();
    public int CurrentPlayerIndex { get; private set; }

    public PlayerData CurrentPlayer => Players.Count > 0
        ? Players[CurrentPlayerIndex]
        : null;

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

    // -----------------------------
    // PLAYER SETUP
    // -----------------------------

    public void CreatePlayers(List<string> names)
    {
        Players.Clear();

        foreach (var name in names)
        {
            Players.Add(new PlayerData(name));
        }

        CurrentPlayerIndex = 0;
    }

    // -----------------------------
    // TURN MANAGEMENT
    // -----------------------------

    public void NextTurn()
    {
        if (Players.Count == 0)
            return;

        CurrentPlayerIndex++;
        if (CurrentPlayerIndex >= Players.Count)
            CurrentPlayerIndex = 0;
    }

    // -----------------------------
    // SCORE MANAGEMENT
    // -----------------------------

    public void AddScoreToCurrentPlayer(int amount)
    {
        CurrentPlayer?.AddScore(amount);
    }
}
