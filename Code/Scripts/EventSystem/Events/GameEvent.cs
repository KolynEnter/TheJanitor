using UnityEngine;
using System.Collections.Generic;


/*
    A SO event triggered without a parameter
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/GameEvent", order = 2)]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> _listeners = new List<GameEventListener>();

        public void TriggerEvent()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered();
            }
        }

        public void AddListener(GameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
