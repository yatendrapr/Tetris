using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Boundary Variables")]
public class TetrisBoundaryVariablesScriptableObject : ScriptableObject
{

    #region Data Members

    #region Global Variables
    
    //Tetris Boundary Variables
    [Header("Tetromino Boundary Variables")]

    [SerializeField] private float positiveXBoundary = 0;
    public float PositiveXBoundary { get { return positiveXBoundary; } }

    [SerializeField] private float negativeXBoundary = 0;
    public float NegativeXBoundary { get { return negativeXBoundary; } }

    [SerializeField] private float negativeYBoundary = 0;
    public float NegativeYBoundary { get { return negativeYBoundary; } }

    [SerializeField] private float positiveYBoundary = 0;
    public float PositiveYBoundary { get { return positiveYBoundary; } }

    #endregion

    #endregion

}
