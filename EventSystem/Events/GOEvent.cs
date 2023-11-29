using UnityEngine;
using System.Collections.Generic;

namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/GOEvent", order = 0)]
    public class GOEvent : ScriptableObject
    {
        private List<GOEventListener> _listeners = new List<GOEventListener>();

        public void TriggerEvent(GameObject go)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(go);
            }
        }

        public void AddListener(GOEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(GOEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
