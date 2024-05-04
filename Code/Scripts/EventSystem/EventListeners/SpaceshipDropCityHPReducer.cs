using UnityEngine;
using UnityEngine.UI;

/*
    Listens to a game event

    Reduce the city hp when the spaceship drops a trash
*/
namespace CS576.Janitor.Process
{
    public class SpaceshipDropCityHPReducer : GameEventListener
    {
        [SerializeField]
        private GoalManager _goalManager;

        public override void OnEventTriggered()
        {
            if (_goalManager.GetGameMode != GameMode.Invasion)
                return;
            
            base.OnEventTriggered();

            _goalManager.ReduceCityHP();
        }
    }
}
