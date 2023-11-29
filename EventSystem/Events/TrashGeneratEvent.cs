using UnityEngine;
using System.Collections.Generic;
using CS576.Janitor.Process;


namespace CS576.Janitor.Trashes
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/TrashGenerateEvent", order = 5)]
    public class TrashGenerateEvent : ScriptableObject
    {
        private List<TrashGenerateListener> _listeners = new List<TrashGenerateListener>();

        public void TriggerEvent(TrashWithGenerateRate[] modifiedTrashGenerateRate, 
                                                            int generateNumber)
        {
            TrashGenerateListener[] listenerArray = _listeners.ToArray();
            ArrayShuffler.Shuffle(listenerArray);

            int counter = 0;
            for (int i = listenerArray.Length - 1; i >= 0; i--)
            {
                bool result = listenerArray[i].OnEventTriggered(modifiedTrashGenerateRate);

                if (counter >= generateNumber)
                    return;
                if (result)
                    counter++;

            }
        }

        public void AddListener(TrashGenerateListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(TrashGenerateListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
