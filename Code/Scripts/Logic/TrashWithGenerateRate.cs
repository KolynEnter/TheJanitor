using CS576.Janitor.Trashes;


/*
    Variable used in game level
    Records the generate chance for a particular trash
*/
namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashWithGenerateRate
    {
        public Trash trash;
        public float generateRate;
    }
}
