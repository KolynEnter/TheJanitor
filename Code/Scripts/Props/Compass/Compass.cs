using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CS576.Janitor.Trashes;


/*
    This class is the green compass arrow
    The compass remains hidden until
        the player has not pick up any trash in 15 seconds
    When triggered, the compass arrow will show and 
    a random trash on the map will be determined.
    If there is such a trash, the compass will determine a
    path between the player's location and the determined
    trash's location. Guiding the player to go to there.
    
    If tehre is no such a trash, the arrow will not show
    and the time counter will be reset.
*/
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

        [SerializeField] private List<WaypointGO> _currentFollowingPath;
        [SerializeField] private Vector3 _currentTrashPosition;
        [SerializeField] private WaypointGO _currentClosestWaypointGOToJanitor;
        [SerializeField] private GameObject _currentLockedTrash;

        private GameObject _currentInstantiatedTrashWaypointGO;

        private CityWaypointPathSearcher _searcher;
        
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

            WaypointGO[] waypointGOs = FindObjectsOfType<WaypointGO>();
            waypointGOs = waypointGOs.Where(x=>x.GetDedication == WaypointDedication.City).ToArray();

            _searcher = new CityWaypointPathSearcher(waypointGOs);
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
            WaypointGO closestWaypointGOToJanitor = _searcher.FindClosestWaypointGOTo(GetJanitorPosition);
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
            MakePath();
        }

        private void MakePath()
        {
            GameObject optionalTrash = FindValidTrash();
            if (optionalTrash == null)
                return;

            _arrowModel.SetActive(true);

            if (_currentInstantiatedTrashWaypointGO != null)
            {
                Destroy(_currentInstantiatedTrashWaypointGO);
            }

            Transform trashTransform = optionalTrash.transform;
            WaypointGO closestWaypointGOToJanitor = _searcher.FindClosestWaypointGOTo(GetJanitorPosition);
            _currentTrashPosition = trashTransform.position;

            WaypointGO closestWaypointGOToTrash = _searcher.FindClosestWaypointGOTo(
                                                        new Vector2(_currentTrashPosition.x, 
                                                                    _currentTrashPosition.z));
            _currentClosestWaypointGOToJanitor = closestWaypointGOToTrash;
            _currentFollowingPath.Clear();

            // There is no path yet, or
            // Player is not following the path
            // Create a new path
            List<WaypointGO> path = _searcher.FindPath(closestWaypointGOToJanitor, closestWaypointGOToTrash);

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
            List<GameObject> trashGOs = TrashTracker.GetTrashGOs;
            if (trashGOs.Count == 0)
            {
                Debug.Log("Could not find even one piece of trash.");
                return null;
            }
            return trashGOs[Random.Range(0, trashGOs.Count-1)];
        }
#nullable disable

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
