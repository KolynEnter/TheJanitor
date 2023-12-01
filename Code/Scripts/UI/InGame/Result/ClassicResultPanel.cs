using UnityEngine;
using UnityEngine.UI;


/*
    The game result panel shown after
    the player has won/lost in classic mode

    This one has a fill bar with the same 
    fill amount as the bar on the top
    (goal bar)
*/
namespace CS576.Janitor.UI
{
    public class ClassicResultPanel : GameResultPanel
    {
        [SerializeField]
        private Image _goalBarFillImage;

        [SerializeField]
        private Image _holdFillImage;

        [SerializeField]
        private BarPointerManager _holdPointerManager;

        public override void ShowPanel()
        {
            base.ShowPanel();
            _holdFillImage.fillAmount = _goalBarFillImage.fillAmount;
            _holdPointerManager.UpdatePointerPosition(_goalBarFillImage.fillAmount);
        }
    }
}
