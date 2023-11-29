using UnityEngine;
using UnityEngine.UI;


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
