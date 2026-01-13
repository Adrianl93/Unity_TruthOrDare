using UnityEngine;
using System.Collections.Generic;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public GameSessionData Data { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Inicialización segura (no fuerza reset en cada carga de escena)
        if (Data == null)
        {
            ResetSession();
        }
    }

    public void ResetSession()
    {
        Data = new GameSessionData
        {
            PlayerNames = new List<string>(),
            AllowAdultContent = true
        };

        Debug.Log("GameSession reiniciada");
    }

    public void ResetForNewGame()
    {
        ResetSession();

        PlayerManager.Instance?.ResetManager();
        ChallengeManager.Instance?.ResetManager();

        Debug.Log("GameSession: reset global de nueva partida");
    }


}
