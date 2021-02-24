using UnityEngine;

public class MinoMovement : MonoBehaviour
{
    //Global Variables
    //Scriptable Objects Refrences
    [SerializeField] private MinoMovementScriptableObject minoMovement = null;
    [SerializeField] private TetrisGridScriptableObject tetrisGrid = null;

    //Local Variables
    //Time Variable
    private float currentTime = 0f;

    //Grid Variable
    public int currentRow = 0;
    public int currentCol = 0;

    //Rotation Variable
    [SerializeField] private Vector3 rotationPointFromPivot = Vector3.zero;

    private void Update()
    {
        MoveDown();
        RotateTetroMino();
        MoveLeftOrRight();
        UpdateRowAndCol();
    }

    private void RotateTetroMino()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.RotateAround(transform.TransformPoint(rotationPointFromPivot), new Vector3(0f, 0f, 1f), 90f);
        }
    }

    private void UpdateRowAndCol()
    {
        tetrisGrid.AddToGrid(currentRow, currentCol);
    }

    private void MoveLeftOrRight()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            this.transform.position += new Vector3(-minoMovement.distanceToDisplaceHorizontal, 0f, 0f);
            currentCol -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.position += new Vector3(minoMovement.distanceToDisplaceHorizontal, 0f, 0f);
            currentCol += 1;
        }
    }

    private void MoveDown()
    {
        if(Time.time - currentTime >= minoMovement.minoSpeed)
        {
            currentTime = Time.time;
            this.transform.position += new Vector3(0f, minoMovement.distanceToDisplaceVertical, 0f);
            currentRow += 1;
        }
    }
}
