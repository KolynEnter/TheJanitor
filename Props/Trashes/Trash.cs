using UnityEngine;
using System;

namespace CS576.Janitor.Trashes
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Trash", order = 0)]
    public class Trash : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Name must be unique!")]
        private string _name;
        public string GetName
        {
            get { return _name; }
        }

        private const float LIGHT_WEIGHT_THRESHOLD = 50;

        [SerializeField]
        private TrashType _type;
        public TrashType GetTrashType
        {
            get { return _type; }
        }

        [SerializeField]
        [Tooltip("Measured in g, must be greater than zero.")]
        private float _weight;
        public float GetWeight
        {
            get { return _weight; }
        }

        public bool IsLightWeight
        {
            get { return GetWeight <= LIGHT_WEIGHT_THRESHOLD; }
        }

        [SerializeField]
        private int _barFillValue;
        public int GetBarFillValue
        {
            get { return _barFillValue; }
        }

        [SerializeField]
        private int _scoreValue;
        public int GetScoreValue
        {
            get { return _scoreValue; }
        }

        [SerializeField]
        [Range(0, 100)]
        private float _generateChance;
        public float GetGenerateChance
        {
            get { return _generateChance; }
        }

        [SerializeField]
        [Tooltip("Should trash be grabbed by both hands.")]
        private bool _shouldDoubleHand;
        public bool ShouldDoubleHand
        {
            get { return _shouldDoubleHand; }
        }

        [SerializeField]
        private float _grabbingRadius;
        public float GetGrabbingRadius
        {
            get { return _grabbingRadius; }
        }

        [SerializeField]
        [Tooltip("Preferred rotation when trash is generated.")]
        private Prop.RotationPreference _genRotationPref;
        public Prop.RotationPreference GetGenRotationPref
        {
            get { return _genRotationPref; }
        }

        [SerializeField]
        [Tooltip("Scale adjustment when trash is grabbed by a tool.")]
        private float _toolScaleAdjustment;
        public float GetToolScaleAdjustment
        {
            get { return _toolScaleAdjustment; }
        }

        [SerializeField]
        [Tooltip("Additional position adjustment when trash is grabbed by a tool.")]
        private Vector3 _toolPositionAdjustment;
        public Vector3 GetToolPositionAdjustment
        {
            get { return _toolPositionAdjustment; }
        }

        [SerializeField]
        [Tooltip("Additional rotation adjustment when trash is grabbed by a tool.")]
        private Vector3 _toolRotationAdjustment;
        public Vector3 GetToolRotationAdjustment
        {
            get { return _toolRotationAdjustment; }
        }

        [SerializeField]
        [Tooltip("The scale of trash in UI slot.")]
        private float _itemScale;
        public float GetItemScale
        {
            get { return _itemScale; }
        }

        [SerializeField]
        [Tooltip("The position of trash in UI slot.")]
        private Vector3 _itemPosition;
        public Vector3 GetItemPosition
        {
            get { return _itemPosition; }
        }

        [SerializeField]
        [Tooltip("The rotation of trash in UI slot.")]
        private Vector3 _itemRotation;
        public Vector3 GetItemRotation
        {
            get { return _itemRotation; }
        }

        private void Start()
        {
            if (_weight <= 0)
            {
                throw new Exception("Weight of trash must be higher than zero.");
            }
        }
    }
}
