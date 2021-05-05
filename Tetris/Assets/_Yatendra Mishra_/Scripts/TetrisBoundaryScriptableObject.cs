using UnityEngine;

[CreateAssetMenu(fileName = "Tetromino Boundary",menuName ="Tetromino/Tetromino Boundary")]
public class TetrisBoundaryScriptableObject : ScriptableObject
{

    #region Data Members

    #region Global Variables

    //Scriptable Objects reference
    [SerializeField] private TetrisBoundaryVariablesScriptableObject tetrisBoundary = null;

    #endregion

    #endregion

    #region Member Functions

    //This function checks whether the current position of the tetromino(tetromino's children) are inside boundary
    public bool IsInsideBoundary(Transform transform)
    {
        bool _isInsideBoundary = true;
        foreach (Transform localTransform in transform)
        {
            if ((localTransform.position.x < tetrisBoundary.NegativeXBoundary)
                || (localTransform.position.x > tetrisBoundary.PositiveXBoundary))
            {
                _isInsideBoundary = false;
                break;
            }
            else if (localTransform.position.y < tetrisBoundary.NegativeYBoundary)
            {
                _isInsideBoundary = false;
                break;
            }
        }
        return _isInsideBoundary;
    }

    public bool CheckIfGridExceed(Transform transform)
    {
        foreach (Transform localTransform in transform)
        {
            if (localTransform.position.y >= tetrisBoundary.PositiveYBoundary)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

}
