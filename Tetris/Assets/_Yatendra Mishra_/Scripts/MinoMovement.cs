using UnityEngine;

public class MinoMovement : MonoBehaviour
{
    //Global Variables//

    //Scriptable Objects Refrences
    [SerializeField] private MinoMovementVariablesScriptableObject minoMovement = null;
    [SerializeField] private TetrisGridScriptableObject tetrisGrid = null;
    [SerializeField] private TetrisBoundaryScriptableObject tetrominoBoundary = null;

    //Game Event 
    [SerializeField] private GameEvent gameOverEvent = null;

    //Local Variables//

    //Time Variable
    private float currentTime = 0f;

    //Rotation Variable
    [SerializeField] private Vector3 rotationPointFromPivot = Vector3.zero;

    //Fast Move key Variables
    private bool fastMoveKeyPressed = false;

    /*Current time is initialized here, because by the time the spawner spawns a mino the move down time has already started, 
    and the mino will go down one unit as soon as it spawns*/
    private void Awake()
    {
        currentTime = Time.time;
    }

    //Adds the current transform's children to their corresponding grid positions
    private void Start()
    {
        tetrisGrid.AddToGridParent(this.transform);
    }

    private void Update()
    {
        CheckForFastMoveDown();
        CheckForSingleMoveDown();
        RotateTetrimino();
        MoveLeftOrRight();
    }

    private void CheckForFastMoveDown()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fastMoveKeyPressed = true;
            MoveDown(false, true);
        }
        else
        {
            fastMoveKeyPressed = false;
            MoveDown(false, false);
        }
    }

    private void CheckForSingleMoveDown()
    {
        if(!fastMoveKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveDown(true);
                tetrisGrid.RemoveFromGridParent(this.transform);
                this.transform.position += new Vector3(0f, minoMovement.DistanceToDisplaceVertical, 0f);
                if (tetrisGrid.CheckIfGridPositionOccupied(this.transform) || !tetrominoBoundary.IsInsideBoundary(this.transform))
                {
                    this.transform.position += new Vector3(0f, -minoMovement.DistanceToDisplaceVertical, 0f);
                    if (!tetrominoBoundary.CheckIfGridExceed(this.transform))
                    {
                        tetrisGrid.AddToGridParent(this.transform);
                        tetrisGrid.DeleteFullRowsAndRearrange();
                        MinosSpawner.SpawnMino = true;
                        this.enabled = false;
                    }
                    else
                    {
                        gameOverEvent.RaiseEvent();
                    }

                }
                else
                    tetrisGrid.AddToGridParent(this.transform);
            }
            else
                MoveDown();
        }
    }

    private void RotateTetrimino()
    {
        if(!fastMoveKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                tetrisGrid.RemoveFromGridParent(this.transform);
                this.transform.RotateAround(transform.TransformPoint(rotationPointFromPivot), new Vector3(0f, 0f, 1f), -90f);
                if (tetrisGrid.CheckIfGridPositionOccupied(this.transform) || !tetrominoBoundary.IsInsideBoundary(this.transform))
                {
                    this.transform.RotateAround(transform.TransformPoint(rotationPointFromPivot), new Vector3(0f, 0f, 1f), 90f);
                    tetrisGrid.AddToGridParent(this.transform);
                }
                else
                    tetrisGrid.AddToGridParent(this.transform);
            }
        }
    }

    private void MoveLeftOrRight()
    {
        if(!fastMoveKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                tetrisGrid.RemoveFromGridParent(this.transform);
                this.transform.position += new Vector3(-minoMovement.DistanceToDisplaceHorizontal, 0f, 0f);
                if (tetrisGrid.CheckIfGridPositionOccupied(this.transform) || !tetrominoBoundary.IsInsideBoundary(this.transform))
                {
                    this.transform.position += new Vector3(minoMovement.DistanceToDisplaceHorizontal, 0f, 0f);
                    tetrisGrid.AddToGridParent(this.transform);
                }
                else
                    tetrisGrid.AddToGridParent(this.transform);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                tetrisGrid.RemoveFromGridParent(this.transform);
                this.transform.position += new Vector3(minoMovement.DistanceToDisplaceHorizontal, 0f, 0f);
                if (tetrisGrid.CheckIfGridPositionOccupied(this.transform) || !tetrominoBoundary.IsInsideBoundary(this.transform))
                {
                    this.transform.position += new Vector3(-minoMovement.DistanceToDisplaceHorizontal, 0f, 0f);
                    tetrisGrid.AddToGridParent(this.transform);
                }
                else
                    tetrisGrid.AddToGridParent(this.transform);
            }
        }
        
    }

    //Game Over is just displayed on console, will link it to a game over scene or a panel
    /*MoveDown() will be disabled when the player has clicked a button, designed specifically for a fast move down or instant move down
      Implementing a fast move down till the animation or mechanics is complete for the desired output(move down)*/
    private void MoveDown(bool changeCurrentTime = false,bool fastMoveKeyPressedlocal = false)
    {
        if(changeCurrentTime)
        {
            currentTime = Time.time;
        }
        else
        {
            if (Time.time - currentTime >= (fastMoveKeyPressedlocal ? minoMovement.MinoFallDownTime / minoMovement.MinoFastFallDownTime : minoMovement.MinoFallDownTime))
            {
                currentTime = Time.time;
                tetrisGrid.RemoveFromGridParent(this.transform);
                this.transform.position += new Vector3(0f, minoMovement.DistanceToDisplaceVertical, 0f);
                if (tetrisGrid.CheckIfGridPositionOccupied(this.transform) || !tetrominoBoundary.IsInsideBoundary(this.transform)) 
                {
                    this.transform.position += new Vector3(0f, -minoMovement.DistanceToDisplaceVertical, 0f);
                    if (!tetrominoBoundary.CheckIfGridExceed(this.transform))
                    {
                        tetrisGrid.AddToGridParent(this.transform);
                        tetrisGrid.DeleteFullRowsAndRearrange();
                        MinosSpawner.SpawnMino = true;
                        this.enabled = false;
                    }
                    else
                    {
                        gameOverEvent.RaiseEvent();
                    }
                }
                else
                    tetrisGrid.AddToGridParent(this.transform);
            }
        }
    }

}
