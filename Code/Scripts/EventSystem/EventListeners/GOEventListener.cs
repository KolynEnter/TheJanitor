using UnityEngine;

namespace CS576.Janitor.Process
{
    public class GOEventListener : MonoBehaviour
    {
        [SerializeField]
        private GOEvent _event;

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

        public virtual void OnEventTriggered(GameObject go)
        {
            
        }
    }
}
