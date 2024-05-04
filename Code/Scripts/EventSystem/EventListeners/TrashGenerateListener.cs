using UnityEngine;
using System;
using CS576.Janitor.Trashes;
using CS576.Janitor.Prop;


/*
    Listens to Trash generate event,
    When triggered, generate trash near the host gameobject
*/
namespace CS576.Janitor.Process
{
    [RequireComponent(typeof(Generator))]
    [RequireComponent(typeof(BoxCollider))]
    public class TrashGenerateListener : MonoBehaviour
    {
        [SerializeField]
        private TrashGenerateEvent _event;

        [SerializeField]
        private GameObject[] _generableTrashes;

        [SerializeField]
        private TrashGenerateSource _source;

        private Generator _generator;

        private void Start()
        {
            _generator = this.GetComponent<Generator>();
        }

        private void OnEnable()
        {
            if (_event != null)
            {
                _event.AddListener(this);
            }
        }

        private void OnDisable()
        {
            if (_event != null)
            {
                _event.RemoveListener(this);
            }
        }

        /*
            Receive generate rate for each type of trash
            Job: to generate copy of valid trash prefab near source location
            On generate success: return the created trash copy
            On generate fail: return null
        */
        public virtual GameObject OnEventTriggered(TrashWithGenerateRate[] modifiedTrashGenerateRate)
        {
            foreach (GameObject generableTrash in _generableTrashes)
            {
                TrashObject trashObj = generableTrash.GetComponent<TrashObject>();
                if (trashObj == null)
                    throw new Exception("Cached gameobject is not a trash.");
                
                Trash trash = trashObj.GetTrash;

                float chance = UnityEngine.Random.Range(0, 100);
                
                if (chance >= GetModifiedChance(modifiedTrashGenerateRate, trash)) continue;

                return CreateTrashCopyOf(generableTrash, trash);
            }

            return null;
        }

        private GameObject CreateTrashCopyOf(GameObject generableTrash, Trash trash)
        {
            GameObject instantiatedTrashGO = Instantiate(generableTrash);

            TrashObject newTrashObj = instantiatedTrashGO.GetComponent<TrashObject>();
            if (newTrashObj == null)
                throw new Exception("The newly generated trash does not have trash object.");
            newTrashObj.source = _source;
            TrashTracker.AddTrashGO(instantiatedTrashGO);
            instantiatedTrashGO.transform.position = _generator.GetRandomGeneratePosition;
            instantiatedTrashGO.transform.rotation = _generator.GetRandomRotationWith(trash.GetGenRotationPref);
            instantiatedTrashGO.transform.SetParent(_generator.GetRootParentTransform);

            return instantiatedTrashGO;
        }

        private float GetModifiedChance(TrashWithGenerateRate[] modifiedTrashGenerateRate, 
                                        Trash trash)
        {
            for (int i = 0; i < modifiedTrashGenerateRate.Length; i++)
            {
                if (trash.GetName == modifiedTrashGenerateRate[i].trash.GetName)
                {
                    return modifiedTrashGenerateRate[i].generateRate;
                }
            }
            return trash.GetGenerateChance;
        }
    }
}
