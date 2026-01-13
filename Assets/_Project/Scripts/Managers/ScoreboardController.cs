using System.Linq;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField] private Transform rowsContainer;
    [SerializeField] private ScoreboardRowUI rowPrefab;

    private void Start()
    {
        BuildScoreboard();
    }

    private void BuildScoreboard()
    {
        var players = PlayerManager.Instance.Players
            .OrderByDescending(p => p.Score)
            .ToList();

        for (int i = 0; i < players.Count; i++)
        {
            var row = Instantiate(rowPrefab, rowsContainer);
            row.SetData(i + 1, players[i]);
        }
    }
}
