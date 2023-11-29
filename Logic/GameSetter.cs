using UnityEngine;
using System.Linq;


/*
    Initializes the current game once the player enters a game scene
    i.e. Harborview
*/
namespace CS576.Janitor.Process
{
    public class GameSetter : MonoBehaviour
    {
        private GameMode _gameMode;
        public GameMode GetGameMode
        {
            get
            {
                return _gameMode;
            }
        }

        [SerializeField]
        private GoalManager _goalManager;

        [SerializeField]
        private GameObject _goalBar;

        [SerializeField]
        private GameObject _scoreLabel;

        [SerializeField]
        private GameConfiguration _config;
        public GameConfiguration GetConfig
        {
            get { return _config; }
        }

        private GameLevel _currentGameLevel;
        public GameLevel GetGameLevel
        {
            get { return _currentGameLevel; }
        }

        [SerializeField]
        private GameLevel[] _allClassicLevels;

        [SerializeField]
        private GameLevel[] _allScoreLevels;

        [SerializeField]
        private GameLevel[] _allInvasionLevels;

        private void Start()
        {
            Initialize();

            Debug.Log("Initializing other components...");

            IRequireGameSetterInitialize[] componentsWithInterface = 
                FindComponentsWithInterface<IRequireGameSetterInitialize>();

            foreach (IRequireGameSetterInitialize gameObject in componentsWithInterface)
            {
                gameObject.Initialize(this);
                Debug.Log(gameObject + " has been initialized.");
            }
        }

        /*
            I grabbed this code from online
            Originally I had trouble having to initialize this class before
            every other monobehaviour class that depends on it.
            I found a solution in the unity forums
            https://forum.unity.com/threads/the-correct-way-to-
            handle-script-execution-order.499969/
            Look for #7 the comment made by user McDev02
            This has inspired me to use interface to initialize the classes,
            I set the interface to IRequireGameSetterInitialize.
            Then, to search all MonoBehaviour that has implemented
            IRequireGameSetterInitialize, I found the answer on
            https://stackoverflow.com/questions/49329764/get-all-
            components-with-a-specific-interface-in-unity.
            The answer made by user derHugo - Simplified filter using Linq...
        */
        private T[] FindComponentsWithInterface<T>() where T : class
        {
            return FindObjectsOfType<MonoBehaviour>(true).OfType<T>().ToArray();
        }

        private void ChangeMode(GameMode mode)
        {
            _gameMode = mode;
            if (mode == GameMode.Classic)
            {
                _goalBar.SetActive(true);
                _scoreLabel.SetActive(false);
                _goalManager.SetGoalTextForClassic();
            }
            else if (mode == GameMode.Score)
            {
                _goalBar.SetActive(false);
                _scoreLabel.SetActive(true);
                _goalManager.SetGoalTextForScore();
            }
            else if (mode == GameMode.Invasion)
            {


                _goalManager.SetGoalTextForInvasion();
            }
        }

        private void PickRandomAccordingLevel(GameMode mode, GameDifficulty difficulty)
        {
            GameLevel[] levels = mode == GameMode.Classic ? _allClassicLevels :
                                    mode == GameMode.Score ? _allScoreLevels :
                                                                _allInvasionLevels;

            GameLevel[] filteredLevels = levels.Where(x=>x.GetDifficulty==difficulty).ToArray();
            _currentGameLevel = filteredLevels[Random.Range(0, filteredLevels.Length)];

            Debug.Log("Game difficulty: " + _currentGameLevel.GetLimitMinutes);
            Debug.Log("Limit minutes: " + _currentGameLevel.GetDifficulty);
        }

        public void Initialize()
        {
            Debug.Log("Game setter initialized.");
            ChangeMode(_config.gameMode);
            PickRandomAccordingLevel(_gameMode, _config.gameDifficulty);
        }
    }
}
