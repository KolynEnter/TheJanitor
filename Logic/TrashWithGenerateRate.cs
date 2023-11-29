using CS576.Janitor.Trashes;

namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashWithGenerateRate
    {
        public Trash trash;
        public float generateRate;
    }
}