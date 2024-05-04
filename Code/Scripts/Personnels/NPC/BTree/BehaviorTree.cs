using UnityEngine;


namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName="ScriptableObjects/Node/BehaviorTree")]
    public class BehaviorTree : ScriptableObject
    {
        public Node rootNode;
        public State treeState = State.Running;
        private bool _hasRootNode;

        public State Update()
        {
            if (!_hasRootNode)
            {
                _hasRootNode = rootNode != null;
                if (!_hasRootNode)
                {
                    Debug.LogWarning($"{name} requires a root node.");
                }
            }

            if (_hasRootNode)
            {
                if (treeState == State.Running)
                {
                    treeState = rootNode.Update();
                }
            }
            else
            {
                treeState = State.Failure;
            }

            return treeState;
        }
    }
}
