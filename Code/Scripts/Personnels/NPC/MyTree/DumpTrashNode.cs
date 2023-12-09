using System.Collections;
using CS576.Janitor.Process;
using UnityEngine;
using static CS576.Janitor.NPC.People;


/*
    Makes NPC drops one piece of trash near him/her
    Trash Generate Listener not directly controlled by
    its host event. Instead controlled by this script.
*/
namespace CS576.Janitor.NPC
{
    public class DropTrashNode : ActionNode
    {
        public float alertDistance;
        public Transform npcTransform;
        public TrashWithGenerateRate[] modifiedTrashGenerateRate;

        private TrashGenerateListener _trashGenerateListener;
        private TrashGenerateListener GetTrashGenerateListener
        {
            get 
            { 
                if (_trashGenerateListener == null)
                {
                    _trashGenerateListener = npcTransform.GetComponent<TrashGenerateListener>();
                }
                return _trashGenerateListener; 
            }
        }

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

        private bool _hasDumpTrashAnimationEnded = false;

        protected override void OnStart()
        {
            _hasDumpTrashAnimationEnded = false;

            // Cannot dump trash near player / police
            if (HasPlayerNearby() || HasPoliceNearby())
            {
                _hasDumpTrashAnimationEnded = true;
                return;
            }

            GameObject result = GetTrashGenerateListener.OnEventTriggered(modifiedTrashGenerateRate);

            if (result != null)
                npcTransform.GetComponent<People>().StartCoroutine(PlayDumpTrashAnimation());
        }

        private IEnumerator PlayDumpTrashAnimation()
        {

            GetAnimator.SetTrigger("DumpTrash");

            yield return new WaitForSeconds(2f);
            yield return null;

            _hasDumpTrashAnimationEnded = true;
            GetAnimator.ResetTrigger("DumpTrash");
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (!_hasDumpTrashAnimationEnded)
                return State.Running;
            return State.Success;
        }

        private bool HasPlayerNearby()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return Vector3.Distance(player.transform.position, npcTransform.position) <= alertDistance;
        }

        private bool HasPoliceNearby()
        {
            People[] people = GameObject.FindObjectsOfType<People>();
            for(int i = 0; i < people.Length; i++)
            {
                if (people[i].GetOccupation == Occupation.Police)
                {
                    if (Vector3.Distance(people[i].transform.position, npcTransform.position) <= alertDistance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
