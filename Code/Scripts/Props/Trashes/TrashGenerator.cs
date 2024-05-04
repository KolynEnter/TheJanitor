using UnityEngine;
using CS576.Janitor.Process;


/*
    Trash generator is responsible for generating trash on the board

    At the same time, it controls the number of trash on the board
    by generating only necessary number of trash.

    At any given time, the number of trash on map should be greater than
        minTrashNumber.
    and the number of trash on map should be smaller than
        maxTrashNumber.
*/
namespace CS576.Janitor.Trashes
{
    public class TrashGenerator : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private TrashGenerateEvent _trashGenerateEvent;

        private TrashWithGenerateRate[] _modifiedTrashGenerateRate;

        private int _minTrashNumber;
        private int _maxTrashNumber;
        private float _trashGenerateInterval;
        private int _trashGenerateNumber;

        private Timer _trashGenerateIntervalTimer;
        private bool _isInitialized = false;

        private void Awake()
        {
            _isInitialized = false;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;

            // maintain min
            if (TrashTracker.GetCurrentTrashNumber < _minTrashNumber)
            {
                _trashGenerateEvent.TriggerEvent(_modifiedTrashGenerateRate, _trashGenerateNumber);
            }
            
            if (!_trashGenerateIntervalTimer.IsTimeOut())
            {
                _trashGenerateIntervalTimer.ElapseTime();
                return;
            }

            // maintain max
            int generateNumber = _trashGenerateNumber + TrashTracker.GetCurrentTrashNumber;
            if (generateNumber > _maxTrashNumber)
            {
                _trashGenerateEvent.TriggerEvent(_modifiedTrashGenerateRate, 
                                                    _maxTrashNumber - TrashTracker.GetCurrentTrashNumber);
            }
            else
            {
                _trashGenerateEvent.TriggerEvent(_modifiedTrashGenerateRate, _trashGenerateNumber);
            }
            _trashGenerateIntervalTimer.Reset();
        }

        public void Initialize(GameSetter gameSetter)
        {
            GameLevel level = gameSetter.GetGameLevel;
            _minTrashNumber = level.GetMinTrashNumber;
            _maxTrashNumber = level.GetMaxTrashNumber;
            _trashGenerateInterval = level.GetTrashGenerateInterval;
            _trashGenerateNumber = level.GetTrashGenerateNumber;

            _trashGenerateIntervalTimer = new Timer(_trashGenerateInterval);

            _modifiedTrashGenerateRate = level.GetModifiedTrashGenerateRate;
            _trashGenerateEvent.TriggerEvent(_modifiedTrashGenerateRate, _trashGenerateNumber);

            _isInitialized = true;
        }
    }
}
