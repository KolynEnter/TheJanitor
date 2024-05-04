using UnityEngine;


namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Node/SequencerNode")]
    public class SequencerNode : CompositeNode
    {
        private int _current;

        protected override void OnStart()
        {
            _current = 0;
        }

        protected override void OnStop()
        {
            _current = 0;
        }

        protected override State OnUpdate()
        {
            if (children == null && children.Count < 1)
            {
                Debug.LogWarning("Sequencer has no children.");
                return State.Failure;
            }

            switch (children[_current]!.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    _current++;
                    break;
                case State.Failure:
                    return State.Failure;
                default:
                    return State.Failure;
            }

            return _current == children.Count ? State.Success : State.Running;
        }
    }
}
