using UnityEngine;

namespace CS576.Janitor.Process
{
    public class IntEventListener : MonoBehaviour
    {
        [SerializeField]
        private IntEvent _event;

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

        public virtual void OnEventTriggered(int val)
        {
            
        }
    }
}
