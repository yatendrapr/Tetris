using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Boundary Variables")]
public class TetrisBoundaryScriptableObject : ScriptableObject
{
    //Global Variables
    //Tetris Boundary Variables
    [Header("Tetromino Boundary Variables")]
    public float negativeXBoundary = 0f;
    public float positiveXBoundary = 0f;
    public float negativeYBoundary = 0f;
    public float positiveYBoundary = 0f;
}
