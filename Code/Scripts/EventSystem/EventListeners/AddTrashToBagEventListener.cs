using UnityEngine;
using CS576.Janitor.Prop;
using CS576.Janitor.Process;


/*
    Listens to a GameObject event (GOEvent)
    The received game object must be a trash

    updates the trash in trash bag (both data & UI)
*/
namespace CS576.Janitor.Trashes
{
    public class AddTrashToBagEventListener : GOEventListener
    {
        [SerializeField]
        private TrashBag _trashBag;

        [SerializeField]
        private TrashSlot[] _slots;

        [SerializeField]
        private UI.CapacityUIManager _capUIManager;

        [SerializeField]
        private StringEvent _chatEvent;

        public override void OnEventTriggered(GameObject go)
        {
            TrashObject trashObj = go.GetComponent<TrashObject>();
            if (trashObj == null)
            {
                return;
            }
            
            Trash trash = trashObj.GetTrash;
            _trashBag.Increment(trash);

            int trashIndex = (int) _trashBag.FindIndexOf(trash);
            // change the quantity label of trash in ui
            _slots[trashIndex].ChangeQuantity(_trashBag.GetNumberOf(trash));
            // instantiate a completely new copy for 3d trash UI representation
            _slots[trashIndex].SetTrashSlot(Instantiate(go));

            // Updates the bag capacity since a new trash is added
            _capUIManager.UpdateUI(_trashBag.GetCurrentWeight, _trashBag.GetTotalCapacity);

            // telling the player a new trash is added to the bag
            _chatEvent.TriggerEvent(go.ToString());
        }
    }
}
