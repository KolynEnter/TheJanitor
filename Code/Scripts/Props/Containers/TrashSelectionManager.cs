using UnityEngine;


/*
    Controls the currently selected trash slot
    It will also update the UI cursor in the trash bag

    Listens to keyboard keys 1, 2, 3, 4, 5
*/
namespace CS576.Janitor.Prop
{
    public class TrashSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private TrashSlot[] _slots;

        [SerializeField]
        private Process.IntVariable _currentTrashSlotIndex;

        private Process.Timer _timer = new Process.Timer(0.2f);

        private void Update()
        {
            if (!_timer.IsTimeOut())
            {
                _timer.ElapseTime();
                return;
            }

            PressKey(KeyCode.Alpha1, 0);
            PressKey(KeyCode.Alpha2, 1);
            PressKey(KeyCode.Alpha3, 2);
            PressKey(KeyCode.Alpha4, 3);
            PressKey(KeyCode.Alpha5, 4);
        }

        private void PressKey(KeyCode keyCode, int slotIndex)
        {
            if (Input.GetKeyDown(keyCode))
            {
                _currentTrashSlotIndex.value = slotIndex;

                for (int i = 0; i < _slots.Length; i++)
                {
                    if (i != slotIndex)
                    {
                        _slots[i].HidePointer();
                    }
                    else
                    {
                        _slots[i].ShowPointer();
                    }
                }
                _timer.Reset();
            }
        }
    }
}
