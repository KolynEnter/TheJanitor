using System;
using System.IO;
using CS576.Janitor.Process;


namespace CS576.Janitor.UI
{
    public class ScoreFileReader
    {
        public LeaderboardEntry[] ReadAllEntries(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    LeaderboardEntry[] leaderboardEntries = new LeaderboardEntry[lines.Length];

                    int counter = 0;
                    foreach (string line in lines)
                    {
                        string[] tuple = line.Split("##");
                        GameDifficulty difficulty = ConvertToGameDifficulty(tuple[0]);
                        string name = tuple[1];
                        int score = int.Parse(tuple[2]);

                        leaderboardEntries[counter] = new LeaderboardEntry
                        {
                            playerName = name,
                            gameDifficulty = difficulty,
                            score = score
                        };

                        counter++;
                    }

                    return leaderboardEntries;
                }
                catch (IOException)
                {
                    return new LeaderboardEntry[0];
                }
            }
            else
            {
                throw new Exception("The file does not exist.");
            }
        }

        private GameDifficulty ConvertToGameDifficulty(string s)
        {
            switch(s)
            {
                case "Normal":
                    return GameDifficulty.Normal;
                case "Hard":
                    return GameDifficulty.Hard;
                default:
                    return GameDifficulty.Normal;
            }
        }
    }
}
