using UnityEngine;

[CreateAssetMenu(menuName = "Tetromino/Tetrimino Movement Variables")]
public class MinoMovementVariablesScriptableObject : ScriptableObject
{

    #region Data Members

    #region Global Variables

    [Header("Tetrimino Movement Variables")]
    //Mino Movement Variable

    [SerializeField] private float minoFallDownTime = 0;
    public float MinoFallDownTime { get { return minoFallDownTime; } }

    [Tooltip("Vertical")]
    [SerializeField] private float distanceToDisplaceVertical = 0;
    public float DistanceToDisplaceVertical { get { return distanceToDisplaceVertical; } }

    [Tooltip("Horizontal")]
    [SerializeField] private float distanceToDisplaceHorizontal = 0;
    public float DistanceToDisplaceHorizontal { get { return distanceToDisplaceHorizontal; } }

    [SerializeField] private int minoFastFallDownTime = 0;
    public int MinoFastFallDownTime { get { return minoFastFallDownTime; } }

    #endregion

    #endregion

}
