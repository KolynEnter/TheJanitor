using UnityEngine;
using System.Collections.Generic;
using CS576.Janitor.Trashes;


/*
    The effective area of the tool 'trashpopper'
    Represented as the white lighting circle in front
    of the player when he is holding trashpopper gun.

    Keeps a list of trash within range.
    Attempts to attract them to the mouth of the
    trashpopper gun every 0.5 seconds.
*/
namespace CS576.Janitor.Tools
{
    public class PopperArea : MonoBehaviour
    {
        [SerializeField]
        private TrashPopper _popper;

        private List<TrashObject> _trashObjectsInZone = new List<TrashObject>();

        [SerializeField]
        private MeshRenderer _renderer;


        private void OnEnable()
        {
            Time.fixedDeltaTime = 0.5f;
            _renderer.enabled = true;
        }

        private void OnDisable()
        {
            _renderer.enabled = false;
        }

        private void FixedUpdate()
        {
            if (_popper.gameObject.activeSelf)
            {
                for (int i = 0; i < _trashObjectsInZone.Count; i++)
                {
                    if (_trashObjectsInZone[i] != null)
                    {
                        _popper.Grab(_trashObjectsInZone[i].gameObject);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            GameObject go = collider.gameObject;

            if (go == null)
                return;
                
            TrashObject trashObj = go.GetComponent<TrashObject>();
            if (trashObj == null)
                return;
            
            _trashObjectsInZone.Add(trashObj);
        }

        private void OnTriggerExit(Collider collider)
        {
            GameObject go = collider.gameObject;

            if (go == null)
                return;
                
            TrashObject trashObj = go.GetComponent<TrashObject>();
            if (trashObj == null)
                return;
            
            if (_trashObjectsInZone.Contains(trashObj))
            {
                _trashObjectsInZone.Remove(trashObj);
            }
            
            RemoveInvalidElements();
        }

        private void RemoveInvalidElements()
        {
            for (int i = 0; i < _trashObjectsInZone.Count; i++)
            {
                if (_trashObjectsInZone[i] == null)
                {
                    _trashObjectsInZone.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
