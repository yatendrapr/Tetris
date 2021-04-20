using UnityEngine;

[CreateAssetMenu(menuName ="Game/Score Variables")]
public class ScoreScriptableObject : ScriptableObject
{
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

    public void ResetCurrentScore()
    {
        CurrentSessionScore = 0;
    }

    private void CheckHighScore()
    {
        highestScore = (CurrentSessionScore > highestScore) ? CurrentSessionScore : highestScore;
    }
}
