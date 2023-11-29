using UnityEngine;
using System.Collections.Generic;

namespace CS576.Janitor.Trashes
{
    public static class TrashTracker
    {
        private static List<GameObject> s_trashGOs = new List<GameObject>();
        public static List<GameObject> GetTrashGOs
        {
            get
            {
                return s_trashGOs;
            }
        }

        public static int GetCurrentTrashNumber
        {
            get { return s_trashGOs.Count; }
        }

        public static void AddTrashGO(GameObject trashGO)
        {
            s_trashGOs.Add(trashGO);
        }

        public static void RemoveTrashGO(GameObject trashGO)
        {
            s_trashGOs.Remove(trashGO);
        }
    }
}
