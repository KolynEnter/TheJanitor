using UnityEngine;

/*
    A component of trash gameobjects
    Every trash gameobject must have this
*/
namespace CS576.Janitor.Trashes
{
    public class TrashObject : MonoBehaviour
    {
        [SerializeField]
        private Trash _trash;
        public Trash GetTrash
        {
            get { return _trash; }
        }

        public TrashGenerateSource source;
    }
}
