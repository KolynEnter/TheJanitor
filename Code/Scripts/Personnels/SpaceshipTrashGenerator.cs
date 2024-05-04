using CS576.Janitor.Trashes;
using UnityEngine;


/*
    Listens to Spaceship trash generate event,
*/
namespace CS576.Janitor.Process
{
    public class SpaceshipTrashGenerator : TrashGenerateListener
    {
        [SerializeField]
        private GameObject _spaceshipTrashIndicatorPrefab;

        [SerializeField]
        private GoalManager _goalManager;

        public override GameObject OnEventTriggered(TrashWithGenerateRate[] modifiedTrashGenerateRate)
        {
            // Trash came from a spaceship, add an indicator to it
            GameObject instantiatedTrashGO = base.OnEventTriggered(modifiedTrashGenerateRate);
            while (instantiatedTrashGO == null)
            {
                instantiatedTrashGO = base.OnEventTriggered(modifiedTrashGenerateRate);
            }
            
            GameObject instantiatedIndicator = Instantiate(_spaceshipTrashIndicatorPrefab);
            instantiatedIndicator.transform.SetParent(instantiatedTrashGO.transform);
            instantiatedIndicator.transform.position = 
                instantiatedTrashGO.transform.position;

            AddTemporaryRigbodyToTrash(instantiatedTrashGO);
            return instantiatedTrashGO;
        }

        /*
            Add a rigbody component that will be remoed once the trash
            touches the ground
            This is because need to take advantage of gravity
        */
        private void AddTemporaryRigbodyToTrash(GameObject trashGO)
        {
            Rigidbody rigidbody = trashGO.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.mass = trashGO.GetComponent<TrashObject>().GetTrash.GetWeight;

            /*
                From unity doc:
                https://docs.unity3d.com/ScriptReference/Rigidbody-constraints.html
            */
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX | 
                                    RigidbodyConstraints.FreezePositionZ |
                                    RigidbodyConstraints.FreezeRotation;
            

            GameObject go = new GameObject();
            BoxCollider goCollider = go.AddComponent<BoxCollider>();
            goCollider.isTrigger = true;
            OnGroundRemovalRigidbody removalRb = go.AddComponent<OnGroundRemovalRigidbody>();
            removalRb.goalManager = _goalManager;

            go.transform.SetParent(trashGO.transform);
            go.transform.position = trashGO.transform.position;
        }
    }
}
