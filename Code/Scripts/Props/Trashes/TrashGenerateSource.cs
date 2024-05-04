/*
    The possible sources to generate trash
    Each trash generate listener should have this, becaues 
        we need to indicate the source
    Each trash object should have this, and it should be assigned
    each time a new gameobject with TrashObject is instantiated.
*/
namespace CS576.Janitor.Trashes
{
    public enum TrashGenerateSource
    {
        Pedestrian,
        Spaceship,
        Street
    }
}
