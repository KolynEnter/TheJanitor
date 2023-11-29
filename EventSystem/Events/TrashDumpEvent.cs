using UnityEngine;
using System.Collections.Generic;

namespace CS576.Janitor.Trashes
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/TrashDumpEvent", order = 1)]
    public class TrashDumpEvent : ScriptableObject
    {
        private List<TrashDumpEventListener> _listeners = new List<TrashDumpEventListener>();

        public void TriggerEvent(Trash trash, TrashType canType)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(trash, canType);
            }
        }

        public void AddListener(TrashDumpEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(TrashDumpEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
