using UnityEngine;
using UnityEngine.UI;
using CS576.Janitor.Process;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Tools
{
    public class JammerProducer : TrashDumpEventListener, IRequireGameSetterInitialize
    {
        [SerializeField]
        private Image _jammerDemandFillImage;
        [SerializeField]
        private StringEvent _chatEvent;

        private float _totalJammerProduceDemand;
        private GameMode _gameMode;
        private TrashWithFillValue[] _modifiedTrashBarFillValue;

        private float _currentFillValue;

        public override void OnEventTriggered(Trash trash, TrashType canType)
        {
            if (_gameMode != GameMode.Invasion)
                return;

            if (trash == null)
                return;
            
            int fillValue = trash.GetTrashType == canType ? 
                                        GetModifiedTrashValue(trash) : 
                                        -GetModifiedTrashValue(trash);
            
            if (fillValue > 0)
            {
                // fill demand
                _currentFillValue += fillValue;
                
                while (_currentFillValue >= _totalJammerProduceDemand)
                {
                    AddJammerToPlayer();
                    _currentFillValue -= _totalJammerProduceDemand;
                }
                
                _jammerDemandFillImage.fillAmount = 
                    _currentFillValue / _totalJammerProduceDemand;
            }
        }

        private void AddJammerToPlayer()
        {
            _chatEvent.TriggerEvent("Number of jammer + 1.");
        }

        public void Initialize(GameSetter gameSetter)
        {
            _totalJammerProduceDemand = gameSetter.GetGameLevel.GetJammerProduceDemand;
            _gameMode = gameSetter.GetGameLevel.GetMode;
            _modifiedTrashBarFillValue = gameSetter.GetGameLevel.GetModifiedTrashBarFillValue;
        }

        private int GetModifiedTrashValue(Trash trash)
        {
            for (int i = 0; i < _modifiedTrashBarFillValue.Length; i++)
            {
                if (trash.GetName == _modifiedTrashBarFillValue[i].trash.GetName)
                {
                    return _modifiedTrashBarFillValue[i].fillValue;
                }
            }
            return trash.GetBarFillValue;
        }
    }
}
