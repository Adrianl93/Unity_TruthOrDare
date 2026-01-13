using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        if (GameSession.Instance != null)
        {
            GameSession.Instance.ResetSession();
        }

        SceneManager.LoadScene("CategorySelect");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
