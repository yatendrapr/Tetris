using UnityEngine;

public class GameController : MonoBehaviour
{
    //Global Variables//

    //Scriptable Object Refrences
    [SerializeField] private TetrisGridScriptableObject TetrisGrid = null;

    //Game Events
    [SerializeField] private ScoreScriptableObject gameVariables = null; 

    //Initializes the grid. Each element is initialized with null
    private void Start()
    {
        TetrisGrid.InitializeGrid();
    }

    //Resets the grid and the grid dictonary once the game has been closed
    private void OnDestroy()
    {
        TetrisGrid.ResetGrid();
        gameVariables.ResetCurrentScore();
    }
}
