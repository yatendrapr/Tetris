using UnityEngine;

[CreateAssetMenu(fileName = "Tetromino Boundary",menuName ="Tetromino/Tetromino Boundary")]
public class TetrominoBoundaryScriptableObject : ScriptableObject
{
    //Global Variables
    //Scriptable Objects references
    [SerializeField] private TetrisBoundaryScriptableObject tetrisBoundary = null;

    //This function checks whether the current position of the tetromino(tetromino's children) are inside boundary
    public bool IsInsideBoundary(Transform transform)
    {
        bool _isInsideBoundary = true;
        foreach (Transform localTransform in transform)
        {
            if ((localTransform.position.x < tetrisBoundary.negativeXBoundary)
                || (localTransform.position.x > tetrisBoundary.positiveXBoundary))
            {
                _isInsideBoundary = false;
                break;
            }
            else if (localTransform.position.y < tetrisBoundary.negativeYBoundary)
            {
                _isInsideBoundary = false;
                break;
            }
        }
        return _isInsideBoundary;
    }
}
