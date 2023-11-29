using System;
using System.Linq;
using CS576.Janitor.Process;
using UnityEngine;
using TMPro;


namespace CS576.Janitor.UI
{
    public class Leaderboard : MonoBehaviour
    {
        private string _filePath;

        [SerializeField]
        private GameDifficulty _currentDifficulty = GameDifficulty.Normal;

        [Serializable]
        private struct Person
        {
            public TextMeshProUGUI nameComponent;

            public TextMeshProUGUI scoreComponent;
        }

        [SerializeField]
        private Person[] _people;

        [SerializeField]
        private TextMeshProUGUI _titleComponent;

        private LeaderboardEntry[] _allEntries;

        private ScoreFileReader _reader;

        private void Awake()
        {
            _filePath = "Assets/Level/TextFiles/playerScores.txt";
            _currentDifficulty = GameDifficulty.Normal;
            _reader = new ScoreFileReader();
        }

        private void Start()
        {
            _titleComponent.text = "Score mode, Difficulty Normal";
            _allEntries = _reader.ReadAllEntries(_filePath);
            UpdateEntries();
        }

        private void UpdateEntries()
        {
            LeaderboardEntry[] filteredEntries = _allEntries.Where(x => 
                x.gameDifficulty == _currentDifficulty
            ).ToArray();
            LeaderboardEntry[] orderedEntries = filteredEntries.OrderBy(x => -x.score).ToArray();

            for (int i = 0; i < _people.Length; i++)
            {
                if (i >= orderedEntries.Length)
                {
                    _people[i].nameComponent.text = "";
                    _people[i].scoreComponent.text = "";
                    continue;
                }
                _people[i].nameComponent.text = orderedEntries[i].playerName;
                _people[i].scoreComponent.text = orderedEntries[i].score.ToString();
            }
        }

        public void ChangeGameDifficulty()
        {
            if (_currentDifficulty == GameDifficulty.Normal)
            {
                _currentDifficulty = GameDifficulty.Hard;
                _titleComponent.text = "Score mode, Difficulty Hard";
            }
            else
            {
                _currentDifficulty = GameDifficulty.Normal;
                _titleComponent.text = "Score mode, Difficulty Normal";
            }

            UpdateEntries();
        }
    }
}
