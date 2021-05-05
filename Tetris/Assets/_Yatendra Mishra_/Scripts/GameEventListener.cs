using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    #region Data Members

    #region Global Variables

    [Header("Events")]
    //Game Event
    [SerializeField] private GameEvent gameEvent = null;

    //Unity Event
    [SerializeField] private UnityEvent unityEvent = null;

    #endregion

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        gameEvent.AddGameEventListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveGameEventListener(this);
    }

    #endregion

    #region Member Functions
    
    public void OnEventRaised()
    {
        unityEvent.Invoke();
    }

    #endregion

}
