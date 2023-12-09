using UnityEngine;


namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName="ScriptableObjects/Node/DebugLogNode")]
    public class DebugLogNode : ActionNode
    {
        public string message;

        protected override void OnStart()
        {
            Debug.Log($"OnStart: {message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop: {message}");
        }

        protected override State OnUpdate()
        {
            Debug.Log($"OnUpdate: {message}");
            return State.Running;
        }
    }
}
