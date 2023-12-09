using System.Collections;
using CS576.Janitor.Process;
using CS576.Janitor.Trashes;
using UnityEngine;
using UnityEngine.AI;


/*
    Makes NPC grab a nearby trash
    If there is no trash nearby, return success directly

    Need to walk to trash's position to grab
*/
namespace CS576.Janitor.NPC
{
    public class GrabTrashNode : ActionNode
    {
        public float searchRadius;
        public Transform npcTransform;

        private bool _hasGrabbedTrash = false;
        private GameObject _targetTrashGO = null;

        private Animator _animator = null;
        private Animator GetAnimator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = npcTransform.GetComponent<Animator>();
                }
                return _animator;
            }
        }

        private NavMeshAgent _agent = null;
        private NavMeshAgent GetAgent
        {
            get 
            {
                if (_agent == null)
                {
                    _agent = npcTransform.GetComponent<NavMeshAgent> ();
                }
                return _agent;
            }
        }

        protected override void OnStart()
        {
            _hasGrabbedTrash = false;

            GameObject trashGO = GetOneTrashGOWithinRange();
            if (trashGO == null) // no trash nearby
            {
                _hasGrabbedTrash = true;
                return;
            }
            
            GetAgent.SetDestination(trashGO.transform.position);
            _targetTrashGO = trashGO;
        }

        protected override void OnStop()
        {
            GetAnimator.ResetTrigger("GrabTrash");
            GetAnimator.ResetTrigger("GrabTrashStopped");
        }

        protected override State OnUpdate()
        {
            if (_hasGrabbedTrash || _targetTrashGO == null)
            {
                return State.Success;
            }
            else
            {
                if (_targetTrashGO == null) // trash has disappeared
                {
                    GetAgent.ResetPath();
                    return State.Success;
                }

                if (!GetAgent.pathPending && GetAgent.remainingDistance < 0.1f)
                {
                    npcTransform.GetComponent<People>().StartCoroutine(PlayGrabTrashAnimation());
                }
                return State.Running;
            }
        }

        private IEnumerator PlayGrabTrashAnimation()
        {
            GetAnimator.SetTrigger("GrabTrash");

            yield return new WaitForSeconds(6f);
            yield return null;

            _hasGrabbedTrash = true;
            TrashTracker.RemoveTrashGO(_targetTrashGO);
            Destroy(_targetTrashGO);
            GetAnimator.SetTrigger("GrabTrashStopped");
        }

        private GameObject GetOneTrashGOWithinRange()
        {
            foreach (GameObject go in TrashTracker.GetTrashGOs)
            {
                if (go == null)
                    continue;
                
                if (Vector3.Distance(go.transform.position, npcTransform.position) <= 
                        searchRadius)
                {
                    return go;
                }
            }
            return null;
        }
    }
}
