using System.Collections.Generic;
using UnityEngine;

public class TestPlayerFlow : MonoBehaviour
{
    private void Start()
    {
        // Crear jugadores de prueba
        List<string> names = new List<string>
        {
            "Ana",
            "Juan",
            "Lucas"
        };

        PlayerManager.Instance.CreatePlayers(names);

        Debug.Log("Jugadores creados:");
        PrintCurrentPlayer();

        // Simular ronda cumplida
        PlayerManager.Instance.AddScoreToCurrentPlayer(10);
        Debug.Log("Ronda cumplida (+10)");
        PrintCurrentPlayer();

        // Pasar turno
        PlayerManager.Instance.NextTurn();
        Debug.Log("Siguiente turno");
        PrintCurrentPlayer();

        // Simular ronda no cumplida
        PlayerManager.Instance.AddScoreToCurrentPlayer(-5);
        Debug.Log("Ronda no cumplida (-5)");
        PrintCurrentPlayer();
    }

    private void PrintCurrentPlayer()
    {
        var player = PlayerManager.Instance.CurrentPlayer;
        Debug.Log($"Turno de: {player.Name} | Puntaje: {player.Score}");
    }
}
