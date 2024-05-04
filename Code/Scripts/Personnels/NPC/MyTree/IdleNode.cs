using CS576.Janitor.Process;
using UnityEngine;
using UnityEngine.AI;


/*
    Makes NPC stop for a set of time
*/
namespace CS576.Janitor.NPC
{
    public class IdleNode : ActionNode
    {
        public float assignedIdleTime;
        public Transform npcTransform;

        private Timer _idleTimer;

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
        
        protected override void OnStart() 
        {
            People people = npcTransform.GetComponent<People>();
            if (people != null)
            {
                people.currentMove = People.Move.Idle;
            }
            _idleTimer = new Timer(assignedIdleTime);
            _idleTimer.Reset();
        }

        protected override void OnStop()
        {
            _idleTimer.Reset();
        }

        protected override State OnUpdate()
        {
            if (_idleTimer.IsTimeOut())
            {
                return State.Success;
            }
            else
            {
                GetAgent.speed = 0f;
                GetAnimator.SetFloat("Speed", 0f);
                _idleTimer.ElapseTime();
                return State.Running;
            }
        }
    }
}
