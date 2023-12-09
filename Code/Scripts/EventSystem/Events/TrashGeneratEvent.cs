using UnityEngine;
using System.Collections.Generic;
using CS576.Janitor.Process;


/*
    A SO Event used to notify all gameObject with TrashGenerateListener component 
    (or all gameObjects that can generate trash near itself). When this event is
    triggered, notify the listeners to generate trash. 

    This event also passes the trash generation rate to the listeners.
    When a listener successfully generate a trash, this event will count them and
    use it to control the number of trash generated per trigger.
*/
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
                if (counter >= generateNumber)
                    return;
                GameObject result = listenerArray[i].OnEventTriggered(modifiedTrashGenerateRate);

                if (result != null) // successfully generated trash for this listener
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
