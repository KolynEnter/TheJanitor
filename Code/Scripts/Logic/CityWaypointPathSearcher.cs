using UnityEngine;
using System.Collections.Generic;
using System.Linq;


/*
    Applies path finding algorithm to search a path from 
    the given city waypoints

    1. Can also find the closest waypoint near a given Vector2
    2. Can also generated a random waypoint from the given waypoints
*/
namespace CS576.Janitor.Process
{
    public class CityWaypointPathSearcher
    {
        private WaypointGO[] _waypointGOs;

        public CityWaypointPathSearcher(WaypointGO[] waypointGOs)
        {
            _waypointGOs = waypointGOs;
        }

        public List<WaypointGO> FindPath(WaypointGO startPoint, WaypointGO finalPoint)
        {
            List<WaypointGO> visitedPoints = new List<WaypointGO>();
            List<WaypointGO> result = new List<WaypointGO>();

            if (DFS(startPoint, finalPoint, visitedPoints, result))
            {
                result.Add(startPoint);
                result.Reverse();
            }

            return result;
        }

        private bool DFS(WaypointGO current, 
                        WaypointGO target, 
                        List<WaypointGO> visitedPoints,
                        List<WaypointGO> path)
        {
            visitedPoints.Add(current);

            if (current == target)
            {
                return true;
            }

            Vector2 targetPosition = new Vector2(
                                                    target.transform.position.x,
                                                    target.transform.position.z
                                                );

            List<WaypointGO> orderedList = current.GetNeighbors
                                            .OrderBy(waypoint => {
                                                Vector2 wayposition = new Vector2(
                                                    waypoint.transform.position.x,
                                                    waypoint.transform.position.z
                                                );
                                                return (wayposition - targetPosition).magnitude;
                                            }).ToList();

            foreach (WaypointGO neighbor in orderedList)
            {
                if (!visitedPoints.Contains(neighbor) && 
                    DFS(neighbor, target, visitedPoints, path))
                {
                    path.Add(neighbor);
                    return true;
                }
            }

            return false;
        }

        public WaypointGO FindClosestWaypointGOTo(Vector2 position)
        {
            float shortDist = 10000f;
            WaypointGO result = _waypointGOs[0];
            foreach (WaypointGO waypointGO in _waypointGOs)
            {
                Vector2 wayposition = new Vector2(waypointGO.transform.position.x,
                                                waypointGO.transform.position.z);
                if ((wayposition - position).magnitude < shortDist)
                {
                    shortDist = (wayposition - position).magnitude;
                    result = waypointGO;
                }
            }

            return result;
        }

        public WaypointGO GetRandomWaypoint()
        {
            return _waypointGOs[Random.Range(0, _waypointGOs.Length-1)];
        }
    }
}
