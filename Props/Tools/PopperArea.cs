using UnityEngine;
using System.Collections.Generic;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Tools
{
    public class PopperArea : MonoBehaviour
    {
        [SerializeField]
        private TrashPopper _Popper;

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
            if (_Popper.gameObject.activeSelf)
            {
                for (int i = 0; i < _trashObjectsInZone.Count; i++)
                {
                    if (_trashObjectsInZone[i] != null)
                    {
                        _Popper.Grab(_trashObjectsInZone[i].gameObject);
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
