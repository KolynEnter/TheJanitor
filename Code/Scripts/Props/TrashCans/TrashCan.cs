using UnityEngine;
using System.Collections.Generic;


/*
    This class is the trash can.
    Play the "trash is dumped from the top of the trashcan" animation
    only visual purpose
*/
namespace CS576.Janitor.Trashes
{
    public class TrashCan : MonoBehaviour
    {
        [SerializeField]
        private TrashType _type;

        [SerializeField]
        private TrashDumpEvent _onTrashDump;

        [SerializeField]
        private Transform _dumpTeleport;
        private Vector3 _initialDumpPosition;

        [SerializeField]
        private Transform _dumpDestination;
        private float _destinationPositionY;

        // each tracker tracks a single piece of trash
        // mutliple trackers can work at the same time
        // (many trash drops from the top of the trashcan)
        private List<DumpTracker> _dumpTrackers;

        private void Awake()
        {
            _dumpTrackers = new List<DumpTracker>();
        }

        private void Start()
        {
            _initialDumpPosition = _dumpTeleport.position;
            _destinationPositionY = _dumpDestination.position.y;
        }

        private void Update()
        {
            for (int i = 0; i < _dumpTrackers.Count; i++)
            {
                DumpTracker tracker = _dumpTrackers[i];

                if (!tracker.IsReachingTargetPositionY())
                {
                    tracker.Drop();
                }
                else
                {
                    Destroy(tracker.GetTrashGO);
                    _dumpTrackers.Remove(tracker);
                }
            }
        }

        /*
            Instantiate a NEW gameobject of the same trash being dumped
            Play the dumpping animation with this new gameobject,
            not with the original one
        */
        public void ReceiveTrash(Trash trash, GameObject trashGO)
        {
            if (trashGO != null)
            {
                // Play dump trash animation with trash GameObject
                GameObject instantiatedTrashGO = Instantiate(trashGO);
                instantiatedTrashGO.transform.SetParent(_dumpTeleport);
                instantiatedTrashGO.transform.position = _initialDumpPosition;
                instantiatedTrashGO.transform.localScale = new Vector3(1f, 1f, 1f);
                DumpTrash(instantiatedTrashGO);
            }

            Debug.Log("received " + trash);
            _onTrashDump.TriggerEvent(trash, _type);
        }

        private void DumpTrash(GameObject trashGO)
        {

            DumpTracker tracker = new DumpTracker(trashGO, _destinationPositionY, 1f);
            _dumpTrackers.Add(tracker);
        }
    }
}
