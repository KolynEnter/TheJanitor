using UnityEngine;


/*
    Listens to a StringEvent
*/
namespace CS576.Janitor.Process
{
    public class StringEventListener : MonoBehaviour
    {
        [SerializeField]
        private StringEvent _event;

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

        public virtual void OnEventTriggered(string str)
        {
            
        }
    }
}
