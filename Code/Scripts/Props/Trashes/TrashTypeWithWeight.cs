using System;


/*
    trash type with a weight indicator (light/heavy)
    this is used in tools. Indicating what trash a tool
    can pick up.
    i.e. Trash popper can only pick up light weight paper,
    light weight cans.
*/
namespace CS576.Janitor.Trashes
{
    [Serializable]
    public struct TrashTypeWithWeight
    {
        public TrashType trashType;

        public bool lightOnly;
    }
}
