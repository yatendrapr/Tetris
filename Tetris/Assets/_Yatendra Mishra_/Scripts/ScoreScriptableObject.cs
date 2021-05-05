using UnityEngine;

[CreateAssetMenu(menuName = "Game/Score Variables")]
public class ScoreScriptableObject : ScriptableObject
{

    #region Data Members

    #region Local Variables

    //High Score Variables
    private bool newHighScoreReached = false;
    public bool NewHighScoreReached { get { return newHighScoreReached; } }

    //Score Variables
    private int currentSessionScore = 0;
    public int CurrentSessionScore { get { return currentSessionScore; } set { currentSessionScore = value; } }

    [Header("Score Variables")]
    private int highestScore = 0;
    [Tooltip("The amount of points given for a sucessful row clear")]
    [SerializeField] private int rowClearPoints = 0;

    private string highestScorerName = null;
    public string HighestScorerName { get => highestScorerName; set => highestScorerName = value; }

    #endregion

    #endregion

    #region Unity Methods

    //Hideflags is used so that the data persists through different scenes
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    #endregion

    #region Member Functions

    public void AddScore()
    {
        CurrentSessionScore += rowClearPoints;
        CheckHighScore();
    }

    public void ResetScoreVariables() => currentSessionScore = 0;

    private void CheckHighScore()
    {
        if (currentSessionScore > highestScore)
        {
            highestScore = currentSessionScore;
            newHighScoreReached = true;
        }
    }

    public void SetNewHighScoreReached(bool value) => newHighScoreReached = value;

    public bool CheckIfNewHighScoreReached() => newHighScoreReached;

    public int GetHighestScore() => highestScore;

    public bool CheckIfHighScoreMade()
    {
        if (highestScore == 0)
            return false;
        else
            return true;
    }

    #endregion

}
