using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteViewManager : MonoBehaviour
{
    #region Data Members

    //Global Variables//

    [Header("Scriptable Objects")]
    //Scriptable Object References
    [SerializeField] private ScoreScriptableObject scoreVariable = null;

    //Views References
    [Header("Views References")]
    [SerializeField] private GameCompleteView gameOverView = null;
    [SerializeField] private GameCompleteView gameOverViewNewHighScoreView = null;

    //Local Variables//

    //Local View References
    private GameCompleteView currentGameCompleteView = null;

    #endregion

    #region Unity Messages

    private void Awake()
    {
        if (scoreVariable.CheckIfNewHighScoreReached())
        {
            AssignAndActiveCurrentView(gameOverViewNewHighScoreView);
            scoreVariable.SetNewHighScoreReached(false);
        }
        else
        {
            AssignAndActiveCurrentView(gameOverView);
        }

    }

    #endregion

    #region Member Functions

    public void BackToMainMenu() => SceneManager.LoadScene("Main Menu");

    public void CheckLeaderBoard()
    {
        SceneManager.LoadScene("Options");
        SceneManager.LoadScene("Options UI", LoadSceneMode.Additive);
    }

    private void AssignAndActiveCurrentView(GameCompleteView gameCompleteView)
    {
        gameCompleteView.gameObject.SetActive(true);
        currentGameCompleteView = gameCompleteView;
    }
    #endregion
}
