using TMPro;


/*
    The representation form of the Clock UI
*/
namespace CS576.Janitor.Process
{
    public class ClockUI
    {
        private TextMeshProUGUI _timeText;

        public ClockUI(TextMeshProUGUI timeText)
        {
            this._timeText = timeText;
        }

        public void UpdateClock(string time)
        {
            // Update the UI text content with the provided time
            _timeText.text = time;
        }
    }
}
