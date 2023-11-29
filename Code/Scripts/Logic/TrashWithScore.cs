using CS576.Janitor.Trashes;

namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashWithScore
    {
        public Trash trash;
        public int score;
    }
}