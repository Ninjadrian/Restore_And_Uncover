using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> eventListener = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListener.Count - 1; i >= 0; i--)
        {
            eventListener[i].OnEventRise();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListener.Contains(listener))
        {
            eventListener.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListener.Contains(listener))
        {
            eventListener.Remove(listener);
        }
    }
}
