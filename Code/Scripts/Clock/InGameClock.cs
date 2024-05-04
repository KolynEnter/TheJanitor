using UnityEngine;


/*
    The core of the game clock
    Actually ticks the clock by subtracting the minutes & seconds
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(fileName = "New Clock", 
                        menuName = "ScriptableObjects/Clock/InGameClock", 
                        order = 0)]
    public class InGameClock : ScriptableObject
    {
        /*
            I used a scriptable object here to store game minutes and seconds
            because I do not want to look into the scene to debug the current
            time. 
        */
        [SerializeField]
        private GameTimeVariable _gameTime;

        private ClockUI _clockUI;

        /*
            Property form of minutes in game time
            when set, update the ui as well
        */
        public int GameMinuteTimer
        {
            get { return _gameTime.gameMinuteTimer; }
            set
            {
                _gameTime.gameMinuteTimer = value;
                UpdateUI(InGameTimeDisplay());
            }
        }

        /*
            Property form of seconds in game time
            when set, 
                1. if second <= 0 and minute == 0, there is no time left, do nothing
                2. if second > 0, update the second in game timer
            when set, update the ui as well
        */
        public float GameSecondTimer
        {
            get { return _gameTime.gameSecondTimer; }
            set
            {
                if (value <= 0) // a new minute starts
                {
                    if (GameMinuteTimer == 0)
                        return; // no time left
                    _gameTime.gameSecondTimer = 60f;
                    GameMinuteTimer -= 1;
                }
                else
                {
                    _gameTime.gameSecondTimer = value;
                }
                UpdateUI(InGameTimeDisplay());
            }
        }

        /*
            Does the clock has time left
        */
        public bool HasTimeLeft
        {
            get { return GameMinuteTimer > 0 || GameSecondTimer > 0.1; }
        }

        /*
            This is a scriptable object so it requires help from 
            an acutal monobehaviour class to get initialized
        */
        public void InitializeClock(GameSetter setter, ClockUI clockUI)
        {
            _gameTime.gameMinuteTimer = setter.GetGameLevel.GetLimitMinutes;
            _gameTime.gameSecondTimer = 0;
            _clockUI = clockUI;
        }

        /*
            Showing the player the clock is ticked
            Changes visual representation
        */
        private void UpdateUI(string timeString)
        {
            _clockUI.UpdateClock(timeString);
        }

        /*
            Prettify the time text to mm:ss
        */
        private string InGameTimeDisplay()
        {
            int roundSecond = (int) _gameTime.gameSecondTimer;
            return $"{ConvertToTimeString(_gameTime.gameMinuteTimer)}: {ConvertToTimeString(roundSecond)}";
        }

        /*
            Prettify the time text to mm or ss, 
            The range is ["00", "60"]
        */
        private string ConvertToTimeString(int digit)
        {
            if (digit == 60)
                return "59";
            
            if (digit == -1)
                return "00";
            
            string timeString;
            if (digit <= 9)
            {
                timeString = "0" + digit.ToString();
            }
            else
            {
                timeString = digit.ToString();
            }

            return timeString;
        }
    }
}
