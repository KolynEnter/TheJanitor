using UnityEngine;
using UnityEngine.UI;
using CS576.Janitor.Process;


/*
    Regenerate city hp when a spaceship trash is added to bag
*/
namespace CS576.Janitor.Trashes
{
    public class PickupSpacshipTrashCityHPRegenerator : GameEventListener
    {
        [SerializeField]
        private GoalManager _goalManager;

        public override void OnEventTriggered()
        {
            if (_goalManager.GetGameMode != GameMode.Invasion)
                return;
            
            _goalManager.RegenerateCityHP();
        }
    }
}
