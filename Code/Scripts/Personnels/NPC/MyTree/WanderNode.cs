using CS576.Janitor.Process;
using UnityEngine;
using UnityEngine.AI;


/*
    When this node is started, make the npc go to a random position within
    the given range. After reached the destination, return success.

    Speed = 0.1f: Idling
    Speed <= 1.0f: Walking
    1.0f < Speed <= 4.0f: Slow Running
    Speed > 4.0f: Fast Running
*/
namespace CS576.Janitor.NPC
{
    public class WanderNode : ActionNode
    {
        public float wanderRadius;
        public Transform npcTransform;
        public float assignedSpeed = 0.0f;
        private float _speed = 0.0f;

        private bool _isWalkingOnPath = false;

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
            _isWalkingOnPath = false;
            _speed = assignedSpeed;

            People people = npcTransform.GetComponent<People>();
            if (people != null)
            {
                if (_speed > 1.0f)
                {
                    people.currentMove = People.Move.Run;
                }
                else
                {
                    people.currentMove = People.Move.Walk;
                }
            }
        }

        protected override void OnStop() 
        {
            _isWalkingOnPath = false;
            _speed = assignedSpeed;
        }

        protected override State OnUpdate()
        {
            if (!_isWalkingOnPath)
            {
                Vector3 newPos = RandomNavSphere(npcTransform.position, wanderRadius, -1);
                GetAgent.SetDestination(newPos);
                GetAgent.speed = _speed;
                GetAnimator.SetFloat("Speed", _speed);
                _isWalkingOnPath = true;
            }
            
            if (!GetAgent.pathPending && GetAgent.remainingDistance < 0.1f)
            {
                return State.Success;
            }
            return State.Running;
        }

        /*
            code from unity forum
            https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
            thread #3, user Cnc96
        */
        private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

            return navHit.position;
        }
    }
}
