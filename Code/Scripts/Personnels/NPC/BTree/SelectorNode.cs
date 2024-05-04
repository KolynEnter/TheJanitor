using UnityEngine;
using System.Collections.Generic;


namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Node/SelectorNode")]
    public class SelectorNode : CompositeNode
    {
        public List<float> nodeChances = null; // each 1 ~ 100
        private Node _current = null;
        
        protected override void OnStart()
        {
            ChooseNewCurrentNode();
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (children == null && children.Count < 1)
            {
                Debug.LogWarning("Selector has no children.");
                return State.Failure;
            }

            if (_current == null)
            {
                Debug.LogWarning("No current node is running on the Selector.");
                return State.Failure;
            }

            State currentState = _current.Update();
            if (currentState == State.Failure)
                return State.Failure;
            
            if (currentState == State.Success)
            {
                return State.Success;
            }
            
            return State.Running;
        }

        private void ChooseNewCurrentNode()
        {
            for (int i = 0; i < nodeChances.Count; i++)
            {
                if (i >= children.Count)
                    return;

                float r = Random.Range(1, 100);
                if (nodeChances[i] >= r || i == nodeChances.Count-1)
                {
                    _current = children[i];
                    return;
                }
            }
        }
    }
}
