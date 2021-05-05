using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCompleteViewManager : MonoBehaviour
{

    #region Data Members

    #region Global Variables

    [Header("Scriptable Objects References")]
    //Scriptable Object References
    [SerializeField] private ScoreScriptableObject scoreVariable = null;

    [Header("UI References")]
    [SerializeField] private TMP_InputField highestScorerNameInputField = null;
    [SerializeField] private TextMeshProUGUI highestScoreText = null;
    [SerializeField] private Button okButton = null;

    //Views References
    [Header("Views References")]
    [SerializeField] private GameCompleteView gameOverView = null;
    [SerializeField] private GameCompleteView gameOverViewNewHighScoreView = null;

    #endregion

    #region Local Variables

    //Local View References
    private GameCompleteView currentGameCompleteView = null;

    #endregion

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (scoreVariable.CheckIfNewHighScoreReached())
        {
            AssignAndActiveCurrentView(gameOverViewNewHighScoreView);
            scoreVariable.SetNewHighScoreReached(false);
            highestScoreText.text = $"New High Score \n\n  {scoreVariable.GetHighestScore()}";
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

    public void HighestScoreButtonPressed()
    {
        scoreVariable.HighestScorerName = highestScorerNameInputField.text.ToString();
        okButton.enabled = false;
        highestScorerNameInputField.enabled = false;
    }

    #endregion

}
