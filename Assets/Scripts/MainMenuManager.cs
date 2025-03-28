using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Used to load the character creation scene.
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    // Used to completely exit the application (doesn't work in the Editor, but will work in the built .exe game)
    public void ExitGame()
    {
        Application.Quit();
    }
}