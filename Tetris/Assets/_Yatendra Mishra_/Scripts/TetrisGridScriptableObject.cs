using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Tetris/Tetris Grid Variables")]
public class TetrisGridScriptableObject : ScriptableObject
{
    //Global Variables
    //Scriptable object references
    [SerializeField] private MinoMovementScriptableObject minoMovementScriptableObject = null;

    //Local Variables
    //Grid Variables
    private const int noOfCol = 15;
    private const int noOfRow = 27;

    //Constants
    private const float differenceInPosition = 0.1f;

    //Data Structres
    //Grid
    private Transform[,] tetrisGrid = new Transform[noOfRow, noOfCol];


    public void InitializeGrid()
    {
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = 0; j < noOfCol; j++)
            {
                tetrisGrid[i, j] = null;
            }
        }
    }

    //This function loops through the passed transform's children, and assigns them to their corresponding grid points
    public void AddToGridParent(Transform transform)
    {
        foreach (Transform localTransform in transform)
        {
            tetrisGrid[Round(localTransform.position.y), Round(localTransform.position.x)] = localTransform;
        }
    }

    //This function assigns the passed transform to its corresponding grid point
    private void AddToGridChild(Transform transform)
    {
        tetrisGrid[Round(transform.position.y), Round(transform.position.x)] = transform;
    }

    //This function loops through the passed transform's children, and removes them from their corresponding grid points
    public void RemoveFromGridParent(Transform transform)
    {
        foreach (Transform localTransform in transform)
        {
            tetrisGrid[Round(localTransform.position.y), Round(localTransform.position.x)] = null;
        }
    }

    //This function removes the passed transform from its corresponding grid point
    private void RemoveFromGridChild(Transform childTransform)
    {
        tetrisGrid[Round(childTransform.position.y), Round(childTransform.position.x)] = null;
    }

    /* It checks whether the current grid position occupied by the tetromino(tetromino's children) is a valid positition or not
    Occupied = True
    Not Occupied = False */
    public bool CheckIfGridPositionOccupied(Transform transform)
    {
        bool gridPositionOccupied = false;
        foreach (Transform localTransform in transform)
        {
            if (tetrisGrid[Round(localTransform.position.y), Round(localTransform.position.x)] != null)
            {
                gridPositionOccupied = true;
                break;
            }
        }
        return gridPositionOccupied;
    }

    /*This function checks wheter any of the rows are completly occupied, and if they are, it deletes the entire row 
      and clears the tetris grid of the points which were occupied by the full row*/
    public void CheckIfRowFull()
    {
        int transformCount = 0;
        //fullCount keeps track of how many rows has been cleared
        int fullCount = 0;
        int[] clearedRow = new int[noOfRow];
        int clearedRowIndex = 0;
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = 4; j <= (noOfCol - 2); j++)
            {
                if (tetrisGrid[i, j] != null)
                    transformCount++;
            }
            if (transformCount == 10)
            {
                fullCount++;
                clearedRow[clearedRowIndex++] = i;
                //This loop deletes the entire row//
                for (int j = 4; j <= (noOfCol - 2); j++)
                {
                    Transform localTransform = tetrisGrid[i, j];
                    RemoveFromGridChild(tetrisGrid[i, j]);
                    Destroy(localTransform.gameObject);
                }
                transformCount = 0;
            }
            else
                transformCount = 0;
        }
        if (fullCount > 0)
            //Calling the function from here ensures that the all the full row has been deleted and their respective grid points have been reset.
            RearrangeTetrisGrid(clearedRow);
    }

    //Functionality not yet completed
    //Under Debug
    //This function will rearrange all the remaining minos that were not cleared in CheckIfRowFull() func
    private void RearrangeTetrisGrid(int[] clearedRow)
    {
        int currentFullRow = 0, rowMultiplier = 0;
        for (int i = 0; i < (clearedRow.Length - 1); i++)
        {
            if (clearedRow[i + 1] - clearedRow[i] == 1)
            {
                currentFullRow = clearedRow[i + 1];
                rowMultiplier++;
                continue;
            }
            else
            {
                Transform localTransform = null;
                for (int k = currentFullRow + 1; k < noOfRow; k++)
                {
                    for (int j = 4; j <= (noOfCol - 2); j++)
                    {
                        if (tetrisGrid[i, j] != null)
                        {
                            localTransform = tetrisGrid[i, j];
                            RemoveFromGridChild(tetrisGrid[i, j]);
                            localTransform.position += new Vector3(0f, minoMovementScriptableObject.distanceToDisplaceVertical * (rowMultiplier + 1), 0f);
                            AddToGridChild(localTransform);
                        }
                    }
                }
            }
        }

    }

    //Functionality not Completed
    //Under Debug
    private void GetDictonary(int[] clearedRow)
    {
        int currentFullRow = 0;
        int rowMultiplier = 0;
        Dictionary<int, int> rowToClearDictonary = new Dictionary<int, int>();

        switch (clearedRow.Length)
        {
            case 0:
                //yet to be implemented
                break;
            case 1:
                //yet to be implemented
                break;
            default:
                for (int i = 0; i < clearedRow.Length; i++)
                {
                    if ((i + 1) == (clearedRow.Length - 1))
                    {
                        if ((clearedRow[i] - clearedRow[i - 1]) == 1 && (clearedRow[i + 1] - clearedRow[i]) == 1)
                        {
                            currentFullRow = clearedRow[i + 1];
                            rowMultiplier = clearedRow[i + 1] + 1;
                        }

                    }
                    else
                    {
                        if (clearedRow[i + 1] - clearedRow[i] == 1)
                        {
                            currentFullRow = clearedRow[i + 1];
                            rowMultiplier = clearedRow[i + 1] + 1;
                        }
                        else
                        {
                            rowToClearDictonary.Add(++currentFullRow, ++rowMultiplier);
                            currentFullRow = 0;
                            rowMultiplier = 0;
                        }
                    }
                }
                break;
        }
    }

    /*this function checks whether the (((int)value +1) - value) is less than differenceInPosition. The reason for doing this is sometimes 
     transform position would be 3.9999f and according to the logic of the game, it should be 4, so that it could be a valid grid position*/
    private int Round(float value)
    {
        int higherValue = ((int)value) + 1;
        return ((higherValue - value) < differenceInPosition) ? higherValue : (int)value;
    }

    //Made for debugging purposes. It gives the count of how many transforms are occupying grid point in the tetris grid.
    private void CheckGridOccupationCount()
    {
        int count = 0;
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = 4; j <= (noOfCol - 2); j++)
            {
                if (tetrisGrid[i, j] != null)
                    count++;
            }
        }
        Debug.Log(count);
    }

    public void ResetGrid()
    {
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = 0; j < noOfCol; j++)
            {
                tetrisGrid[i, j] = null;
            }
        }
    }

}
