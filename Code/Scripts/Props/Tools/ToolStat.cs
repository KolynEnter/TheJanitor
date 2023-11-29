using UnityEngine;
using CS576.Janitor.Trashes;

namespace CS576.Janitor.Tools
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Tool/Stat", order = 0)]
    public class ToolStat : ScriptableObject
    {
        [SerializeField]
        private int _toolWeight;
        public int GetToolWeight
        {
            get { return _toolWeight; }
        }

        [SerializeField]
        private float _grabRangeModifier;
        public float GetGrabRangeModifier
        {
            get { return _grabRangeModifier; }
        }

        [SerializeField]
        private TrashTypeWithWeight[] _targetingTrashTypes;
        public TrashTypeWithWeight[] GetTargetignTrashTypes
        {
            get { return _targetingTrashTypes; }
        }

        [SerializeField]
        private Vector3 _relativeHoldPosition;
        public Vector3 GetRelativeHoldPosition
        {
            get { return _relativeHoldPosition; }
        }

        [SerializeField]
        private Vector3 _relativeHoldRotation;
        public Vector3 GetRelativeHoldRotation
        {
            get { return _relativeHoldRotation; }
        }

        [SerializeField]
        private Process.ToolType _toolType;
        public Process.ToolType GetToolType
        {
            get { return _toolType; }
        }
    }
}
