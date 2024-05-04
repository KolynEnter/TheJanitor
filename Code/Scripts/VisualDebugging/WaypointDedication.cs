/*
    Also can be seen as the 'type' of a waypoint
        City: waypoint spread across the city
        Trashcan: records the position of a trashcan
            swap point. Swap a trashcan on the position
            in the beginning of the game
        NPCSpawn: records the position of a NPC
            portal
*/
namespace CS576.Janitor.Process
{
    public enum WaypointDedication
    {
        City,
        TrashCan,
        NPCSpawn
    }
}
