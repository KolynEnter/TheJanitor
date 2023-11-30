using UnityEngine;
using System.Collections.Generic;


/*
    A SO event triggered with a string value
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/StringEvent", order = 3)]
    public class StringEvent : ScriptableObject
    {
        private List<StringEventListener> _listeners = new List<StringEventListener>();

        public void TriggerEvent(string str)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(str);
            }
        }

        public void AddListener(StringEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(StringEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
