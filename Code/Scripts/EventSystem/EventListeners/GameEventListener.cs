using UnityEngine;
using UnityEngine.Events;


/*
    Listens to a GameEvent
    Invokes assigned Unity event when triggered
*/
namespace CS576.Janitor.Process
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _event;

        [SerializeField]
        private UnityEvent _onEventTriggered;

        private void OnEnable()
        {
            if (_event != null)
            {
                _event.AddListener(this);
            }
        }

        private void OnDisable()
        {
            _event.RemoveListener(this);
        }

        public virtual void OnEventTriggered()
        {
            _onEventTriggered.Invoke();
        }
    }
}
