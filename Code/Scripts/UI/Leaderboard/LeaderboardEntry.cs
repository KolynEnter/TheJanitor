using CS576.Janitor.Process;


/*
    Store player name, score & difficulty 
    in score mode.
*/
namespace CS576.Janitor.UI
{
    public struct LeaderboardEntry
    {
        public string playerName;

        public int score;

        public GameDifficulty gameDifficulty;
    }
}
