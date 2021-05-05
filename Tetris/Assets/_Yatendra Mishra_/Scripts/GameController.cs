using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Data Members

    #region Global Variables
    
    [Header("Scriptable Objects Reference")]
    //Scriptable Object Refrences
    [SerializeField] private TetrisGridScriptableObject TetrisGrid = null;
    [SerializeField] private ScoreScriptableObject scoreVariable = null;

    #endregion

    #endregion

    #region Unity Methods

    /*Initializes the grid. Each element is initialized with null. 
    It also sets the score variable's new high score reached to false, this is done
    so that if the player accidently closes the game while running, it will set the 
    aformentioned variable to its default value*/
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

    #endregion

}
