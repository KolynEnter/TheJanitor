using UnityEngine;

namespace CS576.Janitor.Process
{
    public class DamageSpaceshipListener : GameEventListener
    {
        [SerializeField]
        private GoalManager _goalManager;

        public override void OnEventTriggered()
        {
            if (_goalManager.GetGameMode != GameMode.Invasion)
                return;
            
            _goalManager.ReduceSpaceshipHP();
        }
    }
}
