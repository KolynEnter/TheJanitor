using UnityEngine;
using UnityEngine.UI;
using CS576.Janitor.Trashes;


/*
    Listens to a trash dump event
    This class should only work in the invasion mode

    Reduce the city hp if the player has dumped trash into wrong can
*/
namespace CS576.Janitor.Process
{
    public class WrongCanCityHPReducer : TrashDumpEventListener
    {
        [SerializeField]
        private GoalManager _goalManager;

        public override void OnEventTriggered(Trash trash, TrashType canType)
        {
            if (_goalManager.GetGameMode != GameMode.Invasion)
                return;

            if (trash == null)
                return;

            if (trash.GetTrashType != canType)
            {
                _goalManager.ReduceCityHP();
            }
        }
    }
}
