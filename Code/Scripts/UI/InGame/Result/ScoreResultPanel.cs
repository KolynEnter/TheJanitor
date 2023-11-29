using UnityEngine;
using TMPro;
using CS576.Janitor.Process;


namespace CS576.Janitor.UI
{
    public class ScoreResultPanel : GameResultPanel
    {
        [SerializeField]
        private TextMeshProUGUI _nameField;

        [SerializeField]
        private TextMeshProUGUI _namePlaceHolderField;

        [SerializeField]
        private TextMeshProUGUI _scoreComponent;

        [SerializeField]
        private GoalManager _goalManager;

        [SerializeField]
        private GameSetter _gameSetter;

        public override void ShowPanel()
        {
            base.ShowPanel();
            _scoreComponent.text = _goalManager.currentScore.ToString();
        }

        public void PressedRecordButton()
        {
            string name = _nameField.text != "" ? _nameField.text : _namePlaceHolderField.text;

            int score = _goalManager.currentScore;

            GameDifficulty difficulty = _gameSetter.GetGameLevel.GetDifficulty;

            LeaderboardEntry entry = new LeaderboardEntry {
                playerName = name,
                gameDifficulty = difficulty,
                score = score
            };

            new ScoreFileWriter().AddOneNewEntry(entry);
        }
    }
}
