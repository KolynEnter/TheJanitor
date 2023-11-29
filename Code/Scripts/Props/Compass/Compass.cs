using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Process
{
    public class Compass: GOEventListener {
        [SerializeField] private GameObject _arrowModel;
        [SerializeField] private GameObject _janitor;
        private Vector2 GetJanitorPosition
        {
            get
            {
                return new Vector2(
                    _janitor.transform.position.x,
                    _janitor.transform.position.z
                );
            }
        }
        [SerializeField] private float _arrowRotatingSpeed;

        [SerializeField] private float _evokeCompassTime;
        [SerializeField] private float _dryTimer;
        [SerializeField] private float _waypointRange;

        private WaypointGO[] _waypointGOs;

        [SerializeField] private List<WaypointGO> _currentFollowingPath;
        [SerializeField] private Vector3 _currentTrashPosition;
        [SerializeField] private WaypointGO _currentClosestWaypointGOToJanitor;
        [SerializeField] private GameObject _currentLockedTrash;

        private GameObject _currentInstantiatedTrashWaypointGO;
        
        private float _updateRate = 0.25f;

        public override void OnEventTriggered(GameObject go)
        {
            TrashObject trashObj = go.GetComponent<TrashObject>();
            if (trashObj == null)
            {
                return;
            }

            OnPickingupAnyTrash();
        }

        private void Start()
        {
            Time.fixedDeltaTime = _updateRate;

            _waypointGOs = FindObjectsOfType<WaypointGO>();
            _waypointGOs = _waypointGOs.Where(x=>x.GetDedication == WaypointDedication.Trash).ToArray();

            _currentFollowingPath = new List<WaypointGO>();

            _arrowModel.SetActive(false);
        }

        private void Update()
        {
            if (_currentFollowingPath.Count <= 0)
                return;

            AdjustArrowPrecision();

            HandlePlayerNotFollowingPath();

            HandlePlayerFollowingPath();
        }

        private void AdjustArrowPrecision()
        {
            if (_currentFollowingPath.Count == 1)
            {
                // allow the arrow to point in precious direction
                _arrowModel.transform.rotation =
                    Quaternion.Slerp(_arrowModel.transform.rotation, 
                                Quaternion.LookRotation(_currentTrashPosition - _arrowModel.transform.position), 
                                _arrowRotatingSpeed * Time.deltaTime);
            }
            else
            {
                _arrowModel.transform.rotation =
                    Quaternion.Slerp(_arrowModel.transform.rotation, 
                                Quaternion.LookRotation(_currentFollowingPath[0].transform.position - _arrowModel.transform.position), 
                                _arrowRotatingSpeed * Time.deltaTime);
            }
        }

        private void HandlePlayerNotFollowingPath()
        {
            WaypointGO closestWaypointGOToJanitor = FindClosestWaypointGOTo(GetJanitorPosition);
                // the player is not following the path
                if (closestWaypointGOToJanitor != _currentClosestWaypointGOToJanitor && 
                    closestWaypointGOToJanitor != _currentFollowingPath[0])
                {
                    MakePath();
                }
                _currentClosestWaypointGOToJanitor = closestWaypointGOToJanitor;
        }

        private void HandlePlayerFollowingPath()
        {
            // Arrived at the next way point in the graph
            if (InRange(_waypointRange, _currentFollowingPath[0], 
                    new Vector2(_janitor.transform.position.x, 
                                _janitor.transform.position.z)))
            {
                // only the trash is left
                if (_currentFollowingPath.Count == 1)
                {
                    return;
                }
                // start following the next way point
                _currentFollowingPath.Remove(_currentFollowingPath[0]);
            }
        }

        private void FixedUpdate()
        {
            EvokeCompass();
        }

        private void EvokeCompass()
        {
            if (_dryTimer < _evokeCompassTime)
            {
                if (_currentFollowingPath.Count == 0)
                {
                    _dryTimer += _updateRate;
                }
                return;
            }
            else
            {
                _dryTimer = 0f;
            }
            _arrowModel.SetActive(true);
            MakePath();
        }

        private void MakePath()
        {
            GameObject optionalTrash = FindValidTrash();
            if (optionalTrash == null)
                return;

            if (_currentInstantiatedTrashWaypointGO != null)
            {
                Destroy(_currentInstantiatedTrashWaypointGO);
            }

            Transform trashTransform = optionalTrash.transform;
            WaypointGO closestWaypointGOToJanitor = FindClosestWaypointGOTo(GetJanitorPosition);
            _currentTrashPosition = trashTransform.position;

            WaypointGO closestWaypointGOToTrash = FindClosestWaypointGOTo(
                                                        new Vector2(_currentTrashPosition.x, 
                                                                    _currentTrashPosition.z));
            _currentClosestWaypointGOToJanitor = closestWaypointGOToTrash;
            _currentFollowingPath.Clear();

            // There is no path yet, or
            // Player is not following the path
            // Create a new path
            List<WaypointGO> path = FindPath(closestWaypointGOToJanitor, closestWaypointGOToTrash);

            // Procedurely generate a WaypointGO to represent the trash's spot
            GameObject trashWayPointGO = new GameObject("TrashWaypoint");
            _currentInstantiatedTrashWaypointGO = trashWayPointGO;
            trashWayPointGO.AddComponent<WaypointGO>();
            trashWayPointGO.transform.position = _currentTrashPosition;
            path.Add(trashWayPointGO.GetComponent<WaypointGO>());

            _currentFollowingPath = path;
        }

        private GameObject FindValidTrash()
        {
            GameObject findingTrash;

            if (_currentLockedTrash == null)
            {
#nullable enable
            GameObject? optionalTrash = FindOneTrash();
#nullable disable
                if (optionalTrash != null)
                {
                    findingTrash = (GameObject) optionalTrash;
                    _currentLockedTrash = findingTrash;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                findingTrash = _currentLockedTrash;
            }

            return findingTrash;
        }

#nullable enable
        private GameObject? FindOneTrash()
        {
            List<GameObject> trashGOs = Trashes.TrashTracker.GetTrashGOs;
            if (trashGOs.Count == 0)
            {
                return null;
            }
            return trashGOs[Random.Range(0, trashGOs.Count-1)];
        }
#nullable disable

        private List<WaypointGO> FindPath(WaypointGO startPoint, WaypointGO finalPoint)
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

        private WaypointGO FindClosestWaypointGOTo(Vector2 position)
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

        private bool InRange(float range, WaypointGO center, Vector2 position)
        {
            Vector2 centerPosition = new Vector2(center.transform.position.x,
                                            center.transform.position.z);

            return (centerPosition - position).magnitude <= range;
        }

        private void OnPickingupAnyTrash()
        {
            _currentFollowingPath.Clear();
            _currentLockedTrash = null;
            if (_currentInstantiatedTrashWaypointGO != null)
            {
                Destroy(_currentInstantiatedTrashWaypointGO);
            }
            _dryTimer = 0f;
            _arrowModel.SetActive(false);
        }
    }
}