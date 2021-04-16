using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Movement Variables")]
public class MinoMovementVariablesScriptableObject : ScriptableObject
{
    //Global Variables
    //Mino Movement Varialble
    [Header("Tetrimino Movement Variables")]
    public float minoFallDownTime;
    [Tooltip("Vertical")]
    public float distanceToDisplaceVertical;
    [Tooltip("Horizontal")]
    public float distanceToDisplaceHorizontal;
}
