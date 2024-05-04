using CS576.Janitor.Trashes;


/*
    Variable used in game level
    Records the generate chance for a type of trashcan
*/
namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashCanWithChance
    {
        public TrashType trashType;
        public float chance;
    }
}
