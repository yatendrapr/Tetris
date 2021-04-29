using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuViewManager : MonoBehaviour
{

    #region Member Functions

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
        SceneManager.LoadScene("Game UI", LoadSceneMode.Additive);
    }

    public void EnterOptions()
    {
        SceneManager.LoadScene("Options");
        SceneManager.LoadScene("Options UI", LoadSceneMode.Additive);
    }

    public void EnterMainMenu() => SceneManager.LoadScene("Main Menu");

    public void ExitGame() => Application.Quit();

    #endregion

}