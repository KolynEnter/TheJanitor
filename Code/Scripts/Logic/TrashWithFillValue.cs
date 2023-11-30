using CS576.Janitor.Trashes;


/*
    Variable used in game level
    Records the trash fill value for a particular trash
*/
namespace CS576.Janitor.Process
{
    [System.Serializable]
    public struct TrashWithFillValue
    {
        public Trash trash;
        public int fillValue;
    }
}
