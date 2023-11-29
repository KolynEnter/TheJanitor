using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace CS576.Janitor.UI
{
    public class ScoreFileWriter
    {
        public void AddOneNewEntry(LeaderboardEntry entry)
        {
            string path = "Assets/Level/TextFiles/playerScores.txt";
            LeaderboardEntry[] entries = new ScoreFileReader().ReadAllEntries(path);
            List<LeaderboardEntry> entryList = entries.ToList();
            entryList.Add(entry);

            OverwriteScoreFile(path, entryList.ToArray());
        }

        private void OverwriteScoreFile(string filePath, LeaderboardEntry[] entries)
        {
            CleanText(filePath);
            foreach (LeaderboardEntry entry in entries)
            {
                WriteTextToFile(filePath, 
                                entry.gameDifficulty+"##"+entry.playerName+"##"+entry.score);
            }
        }

        private void WriteTextToFile(string filePath, string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(text);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void CleanText(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("");
                }
            }
            catch(Exception)
            {
                
            }
        }
    }
}
