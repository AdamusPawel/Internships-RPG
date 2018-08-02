using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void QuitGameWrapper()
    {
        QuitGame();
    }
}