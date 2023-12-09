using UnityEngine;


namespace CS576.Janitor.Process
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        [SerializeField]
        private BehaviorTree _tree;

        private void Start()
        {
            _tree = ScriptableObject.CreateInstance<BehaviorTree>();

            DebugLogNode log = ScriptableObject.CreateInstance<DebugLogNode>();
            log.message = "Testing 1, 2, 3";

            _tree.rootNode = log;
        }

        private void Update()
        {
            _tree.Update();
        }
    }
}
