using TMPro;
using UnityEngine;


namespace CS576.Janitor.Process
{
    public class GoalManager : MonoBehaviour, IRequireGameSetterInitialize
    {
        private GameMode _gameMode;
        public GameMode GetGameMode
        {
            get { return _gameMode; }
        }

        private int _maxGoal; // used in classic game mode
        public int GetMaxGoal
        {
            get { return _maxGoal; }
        }

        public int currentScore; // used in score game mode

        public int spaceshipHP; // used in invasion game mode

        [SerializeField]
        private TextMeshProUGUI _goalText;

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

        public void Initialize(GameSetter gameSetter)
        {
            _maxGoal = gameSetter.GetGameLevel.GetGoal;
            _gameMode = gameSetter.GetGameLevel.GetMode;
        }
    }
}
