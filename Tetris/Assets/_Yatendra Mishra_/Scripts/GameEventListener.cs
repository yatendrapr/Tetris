using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent = null;
    [SerializeField] private UnityEvent unityEvent = null;

    private void OnEnable()
    {
        gameEvent.AddGameEventListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveGameEventListener(this);
    }

    public void OnEventRaised()
    {
        unityEvent.Invoke();
    }
}
