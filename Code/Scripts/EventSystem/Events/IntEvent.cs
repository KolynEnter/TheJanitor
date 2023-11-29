using UnityEngine;
using System.Collections.Generic;

namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/IntEvent", order = 4)]
    public class IntEvent : ScriptableObject
    {
        private List<IntEventListener> _listeners = new List<IntEventListener>();

        public void TriggerEvent(int val)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(val);
            }
        }

        public void AddListener(IntEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IntEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
