using UnityEngine;

/*
    NC = "Normal classic"
    HC = "Hard classic"

    NS = "Normal score"
    HS = "Hard score"

    NI = "Normal invasion"
    HI = "Hard invasion"
*/

namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/GameLevel", order = 4)]
    public class GameLevel : ScriptableObject
    {
        [Header("Level")]
        [SerializeField]
        private GameMode _mode;
        public GameMode GetMode
        {
            get { return _mode; }
        }

        [SerializeField]
        private GameDifficulty _difficulty;
        public GameDifficulty GetDifficulty
        {
            get { return _difficulty; }
        }

        [Range(1, 50)]
        [SerializeField]
        private int _limitMinutes;
        public int GetLimitMinutes
        {
            get { return _limitMinutes; }
        }

        [Header("Classic")]
        [Tooltip("Only relevant in classic mode")]
        [Range(100, 1000)]
        [SerializeField]
        private int _goal;
        public int GetGoal
        {
            get { return _goal; }
        }

        [Header("Invasion")]
        [Tooltip("Only relevant in invasion mode")]
        [Range(1000, 30000)]
        [SerializeField]
        private float _spaceshipHP;
        public float GetSpaceshipHP
        {
            get { return _spaceshipHP; }
        }

        [Tooltip("The amount of damage alien deals to city each time they drop a trash")]
        [Range(10, 50)]
        private float _spaceshpAttackPower;
        public float GetSpaceshipAttackPower
        {
            get { return _spaceshpAttackPower; }
        }
        

        [Tooltip("Only relevant in invasion mode")]
        [Range(10, 20)]
        [SerializeField]
        private float _spaceshipSpeed;
        public float GetSpaceshipSpeed
        {
            get { return _spaceshipSpeed; }
        }

        [Tooltip("Only relevant in invasion mode")]
        [Range(1000, 2500)]
        [SerializeField]
        private float _cityHP;
        public float GetCityHP
        {
            get { return _cityHP; }
        }

        [Tooltip("The amount of hp city regenerates from picking up trash dropped by alien")]
        [Range(10, 50)]
        private float _cityHPRegeneration;
        public float GetCityHPRegeneration
        {
            get { return _cityHPRegeneration; }
        }

        [Tooltip("Only relevant in invasion mode")]
        [Range(50, 200)]
        [SerializeField]
        private float _jammerProduceDemand;
        public float GetJammerProduceDemand
        {
            get { return _jammerProduceDemand; }
        }

        [Header("Trash")]
        [SerializeField]
        [Tooltip("The lowest number of trash can exist at a give time.")]
        private int _minTrashNumber;
        public int GetMinTrashNumber
        {
            get { return _minTrashNumber; }
        }

        [SerializeField]
        [Tooltip("The highest number of trash can exist at a give time.")]
        private int _maxTrashNumber;
        public int GetMaxTrashNumber
        {
            get { return _maxTrashNumber; }
        }

        [SerializeField]
        [Tooltip("The interval of each call to trash generation, measured in seconds.")]
        private float _trashGenerateInterval;
        public float GetTrashGenerateInterval
        {
            get { return _trashGenerateInterval; }
        }

        [SerializeField]
        [Tooltip("The number of trash generated for each call to trash generation.")]
        private int _trashGenerateNumber;
        public int GetTrashGenerateNumber
        {
            get { return _trashGenerateNumber; }
        }

        [Header("Modifications")]

        [Tooltip("Override the existing generate rate for this trash")]
        [SerializeField]
        private TrashWithGenerateRate[] _modifiedTrashGenerateRate;
        public TrashWithGenerateRate[] GetModifiedTrashGenerateRate
        {
            get { return _modifiedTrashGenerateRate; }
        }

        [Tooltip("Override the existing score for this trash")]
        [SerializeField]
        private TrashWithScore[] _modifiedTrashScore;
        public TrashWithScore[] GetModifiedTrashScore
        {
            get { return _modifiedTrashScore; }
        }

        [Tooltip("Override the existing bar fill value for this trash")]
        [SerializeField]
        private TrashWithFillValue[] _modifiedTrashBarFillValue;
        public TrashWithFillValue[] GetModifiedTrashBarFillValue
        {
            get { return _modifiedTrashBarFillValue; }
        }

        [SerializeField]
        private TrashCanWithChance[] _trashCanChance = new TrashCanWithChance[4];
        public TrashCanWithChance[] GetTrashCanChance
        {
            get { return _trashCanChance; }
        }
    }
}
