using CS576.Janitor.Trashes;


/*
    Variable used in game level
    Records the trash score value for a particular trash
*/
namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashWithScore
    {
        public Trash trash;
        public int score;
    }
}