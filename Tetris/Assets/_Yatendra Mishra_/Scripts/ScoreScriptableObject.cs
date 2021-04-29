using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Score Variables")]
public class ScoreScriptableObject : ScriptableObject
{

    //Global Variables//

    //High Score Variables
    private bool newHighScoreReached = false;
    public bool NewHighScoreReached { get { return newHighScoreReached; } }

    //Local Variables//

    //Score Variables
    private int currentSessionScore = 0;
    public int CurrentSessionScore { get { return currentSessionScore; } set { currentSessionScore = value; } }

    [Header("Score Variables")]
    [SerializeField] private int highestScore = 0;
    [Tooltip("The amount of points given for a sucessful row clear")]
    [SerializeField] private int rowClearPoints = 0;

    public void AddScore()
    {
        CurrentSessionScore += rowClearPoints;
        CheckHighScore();
    }

    public void ResetScoreVariables()
    {
        currentSessionScore = 0;
    }

    private void CheckHighScore()
    {
        if (currentSessionScore > highestScore)
        {
            highestScore = currentSessionScore;
            newHighScoreReached = true;
        }
    }

    public void SetNewHighScoreReached(bool value)
    {
        newHighScoreReached = value;
    }

    public bool CheckIfNewHighScoreReached()
    {
        return newHighScoreReached;
    }
}
