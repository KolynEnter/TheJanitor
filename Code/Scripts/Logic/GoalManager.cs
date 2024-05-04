using CS576.Janitor.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
    Manage all sorts of goals in the game
    As well as the goal text UI
    The UI is only initialized before game
    and remains static after that
*/
namespace CS576.Janitor.Process
{
    public class GoalManager : MonoBehaviour, IRequireGameSetterInitialize
    {
        private GameMode _gameMode;
        public GameMode GetGameMode
        {
            get { return _gameMode; }
        }

        [SerializeField]
        private PanelShift _missionAbortedAnimation;

        [SerializeField]
        private PanelShift _goalFulfillAnimation;

        private int _maxGoal; // used in classic game mode
        public int GetMaxGoal
        {
            get { return _maxGoal; }
        }

        public int currentScore; // used in score game mode

        private float _originalSpaceshipHP; // used in invasion game mode

        private float _originalCityHP; // used in invasion game mode

        [SerializeField]
        private Image _cityHPFillImage;

        [SerializeField]
        private Image _spaceshipHPFillImage;

        private float _cityHPRegenerateAmount;

        private float _spaceshpAttackPower;
        
        private float _cityHP; // used in invasion game mode
        // used in invasion game mode
        private float CityHP 
        {
            get { return _cityHP; }
            set 
            {
                if (GetGameMode != GameMode.Invasion)
                    return; 

                if (value <= 0)
                {
                    // you lose in invasion mode
                    _missionAbortedAnimation.TriggerShift();
                }
                _cityHP = value;
            }
        }
        // used in invasion game mode
        private float GetCityHPPercent 
        {
            get { return (CityHP / _originalCityHP) < 1f ? CityHP / _originalCityHP : 1f; }
        }

        private float _spaceshipHP;
        private float SpaceshipHP
        {
            get { return _spaceshipHP; }
            set
            {
                if (GetGameMode != GameMode.Invasion)
                    return; 

                if (value <= 0)
                {
                    // you won in invasion mode
                    _goalFulfillAnimation.TriggerShift();
                }
                _spaceshipHP = value;
            }
        }
        private float GetSpaceshipHPPercent
        {
            get 
            { 
                return (SpaceshipHP / _originalSpaceshipHP) < 1f ? 
                            SpaceshipHP / _originalSpaceshipHP : 
                            1f;
            }
        }

        private float _initialJammerDamage;

        [SerializeField]
        private TextMeshProUGUI _goalText;

        /*
            Set the goal text (the UI component located on the left upper corner)
            it is an orange rectangle with white text.
        */
        public void SetGoalTextForClassic()
        {
            _goalText.text = "GOAL: CLEAR OUT TRASHES BEFORE TIME RUNS OUT.";
        }

        public void SetGoalTextForScore()
        {
            _goalText.text = "GOAL: GAIN HIGHER SCORE BEFORE TIME RUNS OUT.";
        }

        public void SetGoalTextForInvasion()
        {
            _goalText.text = "GOAL: DEFEAT THE SPACESHIP USING JAMMERS.";
        }

        public void RegenerateCityHP()
        {
            CityHP += _cityHPRegenerateAmount;
            _cityHPFillImage.fillAmount = GetCityHPPercent;
        }

        public void RegenerateCityHPWithAttackPower()
        {
            CityHP += _spaceshpAttackPower;
            _cityHPFillImage.fillAmount = GetCityHPPercent;
        }

        public void ReduceCityHP()
        {
            CityHP -= _spaceshpAttackPower;
            _cityHPFillImage.fillAmount = GetCityHPPercent;
        }

        public void ReduceSpaceshipHP()
        {
            SpaceshipHP -= _initialJammerDamage;
            _spaceshipHPFillImage.fillAmount = GetSpaceshipHPPercent;
        }

        public void Initialize(GameSetter gameSetter)
        {
            _maxGoal = gameSetter.GetGameLevel.GetGoal;
            _gameMode = gameSetter.GetGameLevel.GetMode;

            _originalSpaceshipHP = gameSetter.GetGameLevel.GetSpaceshipHP;
            _originalCityHP = gameSetter.GetGameLevel.GetCityHP;
            CityHP = _originalCityHP;
            SpaceshipHP = _originalSpaceshipHP;
            _spaceshpAttackPower = gameSetter.GetGameLevel.GetSpaceshipAttackPower;
            _cityHPRegenerateAmount = gameSetter.GetGameLevel.GetCityHPRegeneration;
            _initialJammerDamage = gameSetter.GetGameLevel.GetInitialJammerDamage;

            Debug.Log("Spaceship hp set to " + _originalSpaceshipHP);
            Debug.Log("City hp set to " + CityHP);
        }
    }
}
