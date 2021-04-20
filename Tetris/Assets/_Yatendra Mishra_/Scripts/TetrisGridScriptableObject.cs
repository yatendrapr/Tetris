using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Tetris/Tetris Grid Variables")]
public class TetrisGridScriptableObject : ScriptableObject
{
    //Global Variables//

    //Scriptable object references
    [SerializeField] private MinoMovementVariablesScriptableObject minoMovementScriptableObject = null;

    //Game Events
    [SerializeField] private GameEvent rowClearGameEvent = null;

    //Local Variables//

    //Grid Variables
    private const int noOfCol = 15;
    private const int noOfRow = 27;

    //Row Start Point
    //These are the starting and end point(x-axis) of the grid which correspondss with the global position
    private const int xStartingPoint = 4;
    private const int xEndPoint = 13;

    //Grid Constants
    private const float differenceInPosition = 0.1f;

    //Data Structres
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

    /*This function checks whether any of the rows are completly occupied, and if they are, it deletes the entire row 
      and clears the tetris grid of the points which were occupied by the full row*/
    public void DeleteFullRowsAndRearrange()
    {
        int transformCount = 0;
        //fullCount keeps track of how many rows has been cleared
        int fullCount = 0;
        //clearedRow stores all the row that are full 
        List<int> clearedRow = new List<int>();
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = xStartingPoint; j <= xEndPoint; j++)
            {
                if (tetrisGrid[i, j] != null)
                    transformCount++;
            }
            if (transformCount == 10)
            {
                fullCount++;
                clearedRow.Add(i);
                //This loop deletes the entire row//
                for (int j = xStartingPoint; j <= xEndPoint; j++)
                {
                    Transform localTransform = tetrisGrid[i, j];
                    RemoveFromGridChild(tetrisGrid[i, j]);
                    Destroy(localTransform.gameObject);
                }
                //This Game Event increments the score after a successful row clear
                rowClearGameEvent.RaiseEvent();
                transformCount = 0;
            }
            else
                transformCount = 0;
        }
        if (fullCount > 0)
            //Calling the function from here ensures that the all the full row has been deleted and their respective grid points have been reset.
            RearrangeTetrisGrid(clearedRow.ToArray());
    }

    //This function will rearrange all the remaining minos that were not cleared in CheckIfRowFull() func
    private void RearrangeTetrisGrid(int[] clearedRow)
    {
        Transform localTransform = null;
        Dictionary<int, int> clearedRowDictionary = GetRowsToClearDictonary(clearedRow);
        int[] rowsToClear = new List<int>(clearedRowDictionary.Keys).ToArray();
        int[] rowsToClearMultiplier = new List<int>(clearedRowDictionary.Values).ToArray();
        for (int i = rowsToClear.Length - 1; i >= 0; i--)
        {
            for (int j = rowsToClear[i]; j < noOfRow; j++)
            {
                for (int k = xStartingPoint; k <= xEndPoint; k++)
                {
                    if (tetrisGrid[j, k] != null)
                    {
                        localTransform = tetrisGrid[j, k];
                        RemoveFromGridChild(tetrisGrid[j, k]);
                        localTransform.position += new Vector3(0f, minoMovementScriptableObject.DistanceToDisplaceVertical * rowsToClearMultiplier[i], 0f);
                        AddToGridChild(localTransform);
                    }
                    else
                        continue;
                }
            }
        }
    }

    /*This function checks for sequencing in the clearedRow array, and it clears the sequencing to get a single integer which is 1 int bigger than the last sequencing number, and
    for non-sequencing number it gives 1 int bigger, it then adds the numbers acquired from this function and adds it to a dictonary<int,int>, whose key holds the row from which 
    the grid should be cleared, and a multiplier that holds the number of times the rows has to come down
    Example -
    Input - 01245 <= clearedRow which is given as parameter for the function
    Output - [0] = (3,3)
             [1] = (6,2)
    */
    private Dictionary<int, int> GetRowsToClearDictonary(int[] clearedRow)
    {
        int size = clearedRow.Length;
        int localMultiplier = 0;
        //This dictionary keeps record of key and value, where key is, from which row to start decrementing, and value is, how much should the row de
        Dictionary<int, int> clearRowDictonary = new Dictionary<int, int>();

        switch (size)
        {
            case 1:
                clearRowDictonary.Add(clearedRow[0] + 1, localMultiplier + 1);
                break;
            case 2:
                if (clearedRow[1] - clearedRow[0] == 1)
                {
                    clearRowDictonary.Add(clearedRow[1] + 1, localMultiplier + 2);
                }
                else
                {
                    clearRowDictonary.Add(clearedRow[0] + 1, localMultiplier + 1);
                    clearRowDictonary.Add(clearedRow[1] + 1, localMultiplier + 1);
                }
                break;
            default:
                for (int i = 0; i < size - 1; i++)
                {
                    if ((i + 1) == (size - 1))
                    {
                        if ((clearedRow[i + 1] - clearedRow[i]) == 1)
                        {
                            localMultiplier++;
                            clearRowDictonary.Add((clearedRow[i + 1] + 1), localMultiplier + 1);
                        }
                        else
                        {
                            clearRowDictonary.Add(clearedRow[i] + 1, localMultiplier + 1);
                            localMultiplier = 0;
                            clearRowDictonary.Add(clearedRow[i + 1] + 1, localMultiplier + 1);
                        }
                    }
                    else
                    {
                        if ((clearedRow[i + 1] - clearedRow[i]) == 1)
                        {
                            localMultiplier++;
                        }
                        else
                        {
                            clearRowDictonary.Add(clearedRow[i] + 1, localMultiplier + 1);
                            localMultiplier = 0;
                        }
                    }
                }
                break;
        }
        return clearRowDictonary;
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
