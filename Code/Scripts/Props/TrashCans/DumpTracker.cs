using UnityEngine;


/*
    This is for animation of the trash being dumped into the trashcan
    Track dummping of a single piece of trash at a time
*/
namespace CS576.Janitor.Trashes
{
    public struct DumpTracker
    {
        private GameObject _trashGO;
        public GameObject GetTrashGO
        {
            get { return _trashGO; }
        }
        private float _targetPositionY;
        public float GetCurrentPositionY
        {
            get 
            { 
                if (_trashGO == null)
                {
                    return float.NegativeInfinity;
                }
                return _trashGO.transform.position.y; 
            }
        }
        private float _droppingSpeed;

        public DumpTracker(GameObject trashGO, 
                            float targetPositionY, 
                            float droppingSpeed)
        {
            _trashGO = trashGO;
            _targetPositionY = targetPositionY;
            _droppingSpeed = droppingSpeed;
        }

        public void Drop()
        {
            _trashGO.transform.position = 
                new Vector3(_trashGO.transform.position.x,
                            GetCurrentPositionY - _droppingSpeed * Time.deltaTime,
                            _trashGO.transform.position.z);
        }

        public bool IsReachingTargetPositionY()
        {
            return GetCurrentPositionY <= _targetPositionY;
        }
    }
}
