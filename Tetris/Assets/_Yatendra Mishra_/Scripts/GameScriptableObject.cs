using UnityEngine;

[CreateAssetMenu(menuName ="Game/Game Variables")]
public class GameScriptableObject : ScriptableObject
{
    //Local Variables
    //Score Variables
    [Header("Score Variables")]
    public int currentSessionScore = 0;
    public int highestScore = 0;
    [Tooltip("The amount of points given for a sucessful row clear")]
    [SerializeField] private int rowClearPoints = 0;
    public void AddScore()
    {
        currentSessionScore += rowClearPoints;
        CheckHighScore();
    }

    public void ResetCurrentScore()
    {
        currentSessionScore = 0;
    }

    private void CheckHighScore()
    {
        highestScore = (currentSessionScore > highestScore) ? currentSessionScore : highestScore;
    }
}
