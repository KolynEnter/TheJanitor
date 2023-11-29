using UnityEngine;
using TMPro;
using CS576.Janitor.UI;


/*
    The clock component necessary to make the clock exist in the scene
    Used to initialize the clock & tick the clock
*/
namespace CS576.Janitor.Process
{
    public class GameTimeTracker : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private TextMeshProUGUI _timeText;

        [SerializeField]
        private InGameClock _clock;

        [SerializeField]
        private PanelShift _timeOutAnimation;

        private ClockUI _clockUI;

        /* Ticking the clock */
        private void Update()
        {
            if (_clock.HasTimeLeft)
            {
                _clock.GameSecondTimer -= Time.deltaTime;
            }
            else
            {
                _timeOutAnimation.TriggerShift();
            }
        }

        public void Initialize(GameSetter gameSetter)
        {
            _clockUI = new ClockUI(_timeText);
            _clock.InitializeClock(gameSetter, _clockUI);
        }
    }
}
