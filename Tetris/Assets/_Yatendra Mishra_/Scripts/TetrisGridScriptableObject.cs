using UnityEngine;

[CreateAssetMenu(menuName = "Tetris/Tetris Grid Variables")]
public class TetrisGridScriptableObject : ScriptableObject
{
    //Class Variables
    //Grid Variables
    private const int noOfCol = 10;
    private const int noOfRow = 20;

    private bool[,] tetrisGrid = new bool[noOfRow, noOfCol];

    public void AddToGrid(int row, int col)
    {
        tetrisGrid[row, col] = true;
    }

    public void RemoveFromGrid(int row, int col)
    {
        tetrisGrid[row, col] = false;
    }

    public void ResetGrid()
    {
        for (int i = 0; i < noOfRow; i++)
        {
            for (int j = 0; j < noOfCol; j++)
            {
                tetrisGrid[i, j] = false;
            }
        }
    }
}
