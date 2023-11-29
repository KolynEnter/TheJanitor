using UnityEngine;
using CS576.Janitor.Trashes;
using CS576.Janitor.Process;

namespace CS576.Janitor.Prop
{
    public class TrashBag : MonoBehaviour
    {
        [SerializeField]
        private TrashSlot[] _slots;

        [SerializeField]
        private Character.HoldingTool _holdingTool;

        [SerializeField]
        private int _totalCapacity;
        public int GetTotalCapacity
        {
            get
            {
                return _totalCapacity;
            }
        }

        private Container<Trash> _container = new Container<Trash>(5);

        private float _toolsWeight = 0;

        public float GetCurrentWeight
        {
            get
            {
                float totalWeight = _toolsWeight;
                for (int i = 0; i < 5; i++)
                {
#nullable enable
                    Trash? trash = GetTrash(i);
#nullable disable
                    if (trash == null)
                        continue;
                    
                    Trash _trash = (Trash) trash;
                    totalWeight += _trash.GetWeight * GetNumberOf(_trash);
                }

                return totalWeight;
            }
        }

        public void AssignToolsWeight(float toolsWeight)
        {
            _toolsWeight = toolsWeight;
        }

        public void Increment(Trash trash)
        {
            _container.Increment(trash);
        }

        public void Decrement(Trash trash)
        {
            _container.Decrement(trash);

            if (_container.GetNumberOf(trash) <= 0)
            {
                Trash holdTrash = _holdingTool.GetTool.GetOnHoldingTrash;
                if (holdTrash == null)
                    return;
                
                if (holdTrash.Equals(trash))
                {
                    _holdingTool.GetTool.DestroyGrabbingTrash();
                }
            }
        }

        public int GetNumberOf(Trash trash)
        {
            return _container.GetNumberOf(trash);
        }

#nullable enable
        public Trash? GetTrash(int index)
        {
            return _container.GetElement(index);
        }

        public GameObject? GetTrashGO(int index)
        {
            return _slots[index].GetStoredTrashGO;
        }

        public int? FindIndexOf(Trash trash)
        {
            return _container.FindIndexOf(trash);
        }
#nullable disable
    }
}
