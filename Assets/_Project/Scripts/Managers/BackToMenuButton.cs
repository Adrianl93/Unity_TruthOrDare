using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    public void GoToMenu()
    {
        if (GameSession.Instance != null)
        {
            GameSession.Instance.ResetForNewGame();
        }
        Debug.Log("REINICIO TOTAL DEL JUEGO a partir de aquí");
        SceneManager.LoadScene("00MainMenu");
    }
}
