using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSetupController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform inputsParent;
    [SerializeField] private PlayerNameInput inputPrefab;
    [SerializeField] private TMP_Text playerCountText;

    [Header("Player Limits")]
    [SerializeField] private int minPlayers = 1;
    [SerializeField] private int maxPlayers = 8;

    private readonly List<PlayerNameInput> inputs = new();

    private void Start()
    {
        SetPlayerCount(minPlayers);
    }

    public void SetPlayerCount(int count)
    {
        int clampedCount = Mathf.Clamp(count, minPlayers, maxPlayers);

        // UI feedback
        if (playerCountText != null)
            playerCountText.text = $"Players: {clampedCount}";

        ClearInputs();

        for (int i = 0; i < clampedCount; i++)
        {
            var input = Instantiate(inputPrefab, inputsParent);
            input.SetIndex(i + 1);
            inputs.Add(input);
        }
    }

    public void ConfirmPlayers()
    {
        var names = new List<string>();

        foreach (var input in inputs)
        {
            names.Add(input.PlayerName);
        }

        // Guardamos configuración
        GameSession.Instance.Data.PlayerNames = names;

        // Inicializamos runtime
        GameSession.Instance.Data.PlayerNames = names;


        Debug.Log("Jugadores registrados:");
        foreach (var name in names)
            Debug.Log(name);

        
    }
    public List<string> GetPlayerNames()
    {
        var names = new List<string>();

        foreach (var input in inputs)
        {
            string name = input.PlayerName;

            if (string.IsNullOrWhiteSpace(name))
            {
                // fallback automático: Player 1, Player 2, etc.
                name = $"Player {inputs.IndexOf(input) + 1}";
            }

            names.Add(name.Trim());
        }

        return names;
    }


    private void ClearInputs()
    {
        foreach (var input in inputs)
            Destroy(input.gameObject);

        inputs.Clear();
    }
}
