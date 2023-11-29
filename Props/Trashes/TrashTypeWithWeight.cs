using UnityEngine;
using System;

namespace CS576.Janitor.Trashes
{
    [Serializable]
    public struct TrashTypeWithWeight
    {
        public TrashType trashType;

        public bool lightOnly;
    }
}
