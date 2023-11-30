using UnityEngine;


/*
    Listens to a TrashDumpEvent
*/
namespace CS576.Janitor.Trashes
{
    public class TrashDumpEventListener : MonoBehaviour
    {
        [SerializeField]
        private TrashDumpEvent _event;

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


        public virtual void OnEventTriggered(Trash trash, TrashType canType)
        {

        }
    }
}