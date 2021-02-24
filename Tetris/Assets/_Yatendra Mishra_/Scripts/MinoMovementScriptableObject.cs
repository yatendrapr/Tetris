using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Movement Variables")]
public class MinoMovementScriptableObject : ScriptableObject
{
    //Global Variables
    //Mino Movement Varialble
    [Header("Tetrimino Movement Variables")]
    public float minoSpeed;
    public float distanceToDisplaceVertical;
    public float distanceToDisplaceHorizontal;
}
