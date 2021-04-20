using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    //Local Variables//
    //Data Structres
    private List<GameEventListener> gameEventListners = new List<GameEventListener>();

    public void AddGameEventListener(GameEventListener gameEventListener)
    {
        gameEventListners.Add(gameEventListener);
    }

    public void RemoveGameEventListener(GameEventListener gameEventListener)
    {
        gameEventListners.Remove(gameEventListener);
    }

    public void RaiseEvent()
    {
        for (int i = gameEventListners.Count - 1; i >= 0; i--)
        {
            gameEventListners[i].OnEventRaised();
        }
    }
}
