using UnityEngine;


namespace CS576.Janitor.Process
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField]
        private State _state = State.Running;

        [SerializeField]
        private bool _started;
        public int nodeID;

        // Runs when the node first starts running.
        // Initialize the node.
        protected abstract void OnStart();
        // Runs when the node stops.
        // Any clanup that the node may need to do.
        protected abstract void OnStop();
        // Runs every update of the node.
        // returns the state of the node once it finishes
        protected abstract State OnUpdate();

        public State Update()
        {
            if (!_started)
            {
                OnStart();
                _started = true;
            }

            _state = OnUpdate();

            if (_state != State.Failure && _state != State.Success)
                return _state;
            
            OnStop();
            _started = false;
            return _state;
        }
    }
}
