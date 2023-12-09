using UnityEngine;
using System.Linq;
using CS576.Janitor.Process;
using System.Collections.Generic;
using CS576.Janitor.Trashes;


/*
    This class represents the spaceship
    the boss enemy in this game

    IMPORTANT: This class is attached to the shield
*/
namespace CS576.Janitor.Character
{
    public class Spaceship : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private MeshRenderer _shieldMeshRenderer;

        [SerializeField]
        private GameObject _ship;

        private CityWaypointPathSearcher _searcher;

        private WaypointGO GetClosestWaypointToSpaceship
        {
            get { return _searcher.FindClosestWaypointGOTo(transform.position); }
        }

        [SerializeField] 
        private List<WaypointGO> _currentFollowingPath;

        [SerializeField]
        private GameEvent _onSpacehipDropsTrash;

        [SerializeField]
        private TrashGenerateEvent _spaceshipTrashGenerateEvent;

        [SerializeField]
        private StringEvent _chatEvent;

        [SerializeField]
        private GameEvent _onDamageSpaceship;

        private TrashWithGenerateRate[] _modifiedTrashGenerateRate;

        private float _shipSpeed;

        private bool _isInitialized = false;

        private void Start()
        {
            _shieldMeshRenderer.enabled = true;
            WaypointGO[] waypointGOs = FindObjectsOfType<WaypointGO>();
            waypointGOs = waypointGOs.Where(x=>x.GetDedication == WaypointDedication.City).ToArray();

            _searcher = new CityWaypointPathSearcher(waypointGOs);
            _currentFollowingPath = new List<WaypointGO>();
        }

        private void Update()
        {
            if (!_isInitialized)
                return;
            
            if (_currentFollowingPath.Count == 0)
            {
                MakePath();
            }
            else
            {
                FollowPath();
            }
        }

        private void FollowPath()
        {
            if (IsOnPoint())
            {
                DumpTrash();
                _currentFollowingPath.Remove(_currentFollowingPath[0]);
                return;
            }

            Vector3 direction = _currentFollowingPath[0].transform.position - _ship.transform.position;

            direction.Normalize();
            direction = new Vector3(direction.x, 0, direction.z);
            _ship.transform.position += direction * _shipSpeed * Time.deltaTime;
            
            RotateSpaceship();
        }

        /*
            Piece of code found from this github repository and modified
            https://github.com/SebLague/Pathfinding

            Smoothly rotates the spaceship to the next position
        */
        private void RotateSpaceship()
        {
            Vector3 targetDir = _currentFollowingPath[0].transform.position - _ship.transform.position;
            float step = 1f * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(
                _ship.transform.forward,
                targetDir,
                step,
                0.0F
            );
            Quaternion targetRota = Quaternion.LookRotation(newDir, Vector3.up);
            targetRota.x = 0;
            targetRota.z = 0;
            _ship.transform.rotation = targetRota;
        }

        private void DumpTrash()
        {
            _onSpacehipDropsTrash.TriggerEvent();
            _spaceshipTrashGenerateEvent.TriggerEvent(_modifiedTrashGenerateRate, 1);
        }

        private bool IsOnPoint()
        {
            return _currentFollowingPath[0].transform.position.x - _ship.transform.position.x < 2f &&
                    _currentFollowingPath[0].transform.position.z - _ship.transform.position.z < 2f;
        }

        private void MakePath()
        {
            _currentFollowingPath.Clear();
            WaypointGO destination = _searcher.GetRandomWaypoint();

            _currentFollowingPath = _searcher.FindPath(GetClosestWaypointToSpaceship, destination);
        }

        public void HideShield()
        {
            _shieldMeshRenderer.enabled = false;
        }

        public void TakeDamage()
        {
            _chatEvent.TriggerEvent("Spaceship received damage!");
            _onDamageSpaceship.TriggerEvent();
        }

        public void Initialize(GameSetter gameSetter)
        {
            _modifiedTrashGenerateRate = gameSetter.GetGameLevel.GetModifiedTrashGenerateRate;
            _shipSpeed = gameSetter.GetGameLevel.GetSpaceshipSpeed;
            _isInitialized = true;
        }
    }
}
