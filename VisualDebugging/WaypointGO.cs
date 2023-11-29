using UnityEngine;
using System.Collections.Generic;

namespace CS576.Janitor.Process
{
    [ExecuteInEditMode]
    public class WaypointGO : MonoBehaviour
    {
        [SerializeField]
        private List<WaypointGO> _neighbors = new List<WaypointGO>();

        public List<WaypointGO> GetNeighbors
        {
            get
            {
                return _neighbors;
            }
        }

        [SerializeField]
        private WaypointDedication _dedication;
        public WaypointDedication GetDedication
        {
            get { return _dedication; }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = GetColor();
            Gizmos.DrawCube(transform.position, new Vector3(1f, 1f, 1f));

#if UNITY_EDITOR            
            UnityEditor.Handles.Label(transform.position + new Vector3(0f, 0f, 1f), gameObject.name);
#endif
            foreach (WaypointGO neighbor in _neighbors)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }

        private Color GetColor()
        {
            switch (GetDedication)
            {
                case WaypointDedication.Trash:
                    return Color.red;
                case WaypointDedication.TrashCan:
                    return Color.green;
                default:
                    return Color.gray;
            }
        }

/*
        private void OnValidate()
        {
            foreach (WaypointGO neighbor in _neighbors)
            {
                if (!neighbor.ContainsNeighbor(this))
                {
                    neighbor.AddNeighbor(this);
                }
            }
        }
*/
        public bool ContainsNeighbor(WaypointGO other)
        {
            return _neighbors.Contains(other);
        }

        public void AddNeighbor(WaypointGO newNeighbor)
        {
            _neighbors.Add(newNeighbor);
        }
    }
}
