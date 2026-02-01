using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("COPYMainGameplay");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        QuitGame();
    }
}
