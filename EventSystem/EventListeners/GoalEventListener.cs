using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CS576.Janitor.UI;


/*
    This class only updates the UI

    Listens to a IntEvent
    Either the trash's filling value / score value
            If this is negative, it indicates that the player has dumped trash
                into wrong trashcan
            If this is positive, it indicates that the player has dumped trash
                into correct trashcan
    Modify UI components based on the current game mode
*/
namespace CS576.Janitor.Process
{
    public class GoalEventListener : IntEventListener
    {
        [SerializeField]
        private GoalManager _goalManager;

        [SerializeField]
        private Image _goalBarFillImage; // used in classic game mode

        [SerializeField]
        private BarPointerManager _pointerManager; // used in classic game mode

        [SerializeField]
        private TextMeshProUGUI _scoreLabelGUI; // used in score game mode

        [SerializeField]
        private PanelShift _panelShift;

        /*
            Receives int val:
            This val is either the trash's filling value / score value
                If this is negative, it indicates that the player has dumped trash
                    into wrong trashcan
                If this is positive, it indicates that the player has dumped trash
                    into correct trashcan
        */
        public override void OnEventTriggered(int val)
        {
            if (_goalManager.GetGameMode == GameMode.Classic)
            {
                ClassicModeModification(val);
            }
            else if (_goalManager.GetGameMode == GameMode.Score)
            {
                ScoreModeModification(val);
            }
            else if (_goalManager.GetGameMode == GameMode.Invasion)
            {
                // if val is negative, player has dump into the wrong can
                // reduce city hp
                

                // if val is positive, player has dump into the correct can
                // add process to making jammer


            }
        }

        private void ClassicModeModification(int val)
        {
            if (_goalBarFillImage.fillAmount < 1.0f)
            {
                _goalBarFillImage.fillAmount += (float) val / (float)_goalManager.GetMaxGoal;
                if (_goalBarFillImage.fillAmount == 1.0f)
                {
                    // game success
                    _panelShift.TriggerShift();
                }
                _pointerManager.UpdatePointerPosition(_goalBarFillImage.fillAmount);
            }
        }

        private void ScoreModeModification(int val)
        {
            _goalManager.currentScore += val;
            _scoreLabelGUI.text = "Score: " + _goalManager.currentScore.ToString();
        }
    }
}
