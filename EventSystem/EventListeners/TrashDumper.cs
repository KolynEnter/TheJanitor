using UnityEngine;
using CS576.Janitor.Prop;
using CS576.Janitor.Process;

namespace CS576.Janitor.Trashes
{
    public class TrashDumper : TrashDumpEventListener, IRequireGameSetterInitialize
    {
        [SerializeField]
        private TrashBag _trashBag;

        [SerializeField]
        private TrashSlot[] _slots;

        [SerializeField]
        private IntEvent _onGoalUpdates;

        [SerializeField]
        private UI.CapacityUIManager _capUIManager;

        [SerializeField]
        private StringEvent _chatEvent;

        private GameMode _gameMode;
        private TrashWithScore[] _modifiedTrashScore;
        private TrashWithFillValue[] _modifiedTrashBarFillValue;

        public override void OnEventTriggered(Trash trash, TrashType canType)
        {
            int? trashIndex =_trashBag.FindIndexOf(trash);
            if (trashIndex == null)
                return;
            
            UI.AudioManager.Instance.PlaySFX(UI.GameSFX.DumpTrash);
            
            int _trashIndex = (int) trashIndex;
            _trashBag.Decrement(trash);
            _slots[_trashIndex].ChangeQuantity(_trashBag.GetNumberOf(trash));
            if (_trashBag.GetTrash(_trashIndex) == null)
            {
                _slots[_trashIndex].SetTrashSlot(null);
            }

            _onGoalUpdates.TriggerEvent(trash.GetTrashType == canType ? 
                                        GetModifiedTrashValue(_gameMode, trash) : 
                                        -GetModifiedTrashValue(_gameMode, trash));

            _capUIManager.UpdateUI(_trashBag.GetCurrentWeight, _trashBag.GetTotalCapacity);
        }

        private int GetModifiedTrashValue(GameMode mode, Trash trash)
        {
            if (mode == GameMode.Classic || mode == GameMode.Invasion)
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
            else if (mode == GameMode.Score)
            {
                for (int i = 0; i < _modifiedTrashScore.Length; i++)
                {
                    if (trash.GetName == _modifiedTrashScore[i].trash.GetName)
                    {
                        return _modifiedTrashScore[i].score;
                    }
                }
                return trash.GetScoreValue;
            }
            return 0;
        }

        public virtual void Initialize(GameSetter gameSetter)
        {
            _gameMode = gameSetter.GetGameLevel.GetMode;
            _modifiedTrashScore = gameSetter.GetGameLevel.GetModifiedTrashScore;
            _modifiedTrashBarFillValue = gameSetter.GetGameLevel.GetModifiedTrashBarFillValue;
        }
    }
}
