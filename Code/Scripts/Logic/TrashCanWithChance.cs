using CS576.Janitor.Trashes;

namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashCanWithChance
    {
        public TrashType trashType;
        public float chance;
    }
}
