using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Boundary Variables")]
public class TetrisBoundaryVariablesScriptableObject : ScriptableObject
{
    //Global Variables
    //Tetris Boundary Variables
    [Header("Tetromino Boundary Variables")]
    public float positiveXBoundary = 0;
    public float negativeXBoundary = 0;
    public float negativeYBoundary = 0;
    public float positiveYBoundary = 0;
}
