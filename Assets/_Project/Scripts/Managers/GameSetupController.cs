using UnityEngine;
using System.Collections.Generic;

public class GameSetupController : MonoBehaviour
{
    public static GameSetupController Instance { get; private set; }

    public GameSessionData SessionData { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SessionData = new GameSessionData
        {
            PlayerNames = new List<string>(),
            AllowAdultContent = false
        };
    }

    public void SetCategory(GameCategory category)
    {
        SessionData.SelectedCategory = category;
    }

    public void SetPlayerNames(List<string> names)
    {
        SessionData.PlayerNames = names;
    }

    public void SetAdultContent(bool allowed)
    {
        SessionData.AllowAdultContent = allowed;
    }
}
