using UnityEngine;

public class GameController : MonoBehaviour
{
    //Global Variables//

    //Scriptable Object Refrences
    [SerializeField] private TetrisGridScriptableObject TetrisGrid = null;

    //Game Events
    [SerializeField] private ScoreScriptableObject scoreVariable = null; 

    //Initializes the grid. Each element is initialized with null
    private void Start()
    {
        TetrisGrid.InitializeGrid();
        scoreVariable.SetNewHighScoreReached(false);
    }

    //Resets the grid and the grid dictonary once the game has been closed
    private void OnDestroy()
    {
        TetrisGrid.ResetGrid();
        scoreVariable.ResetScoreVariables();
    }
}
