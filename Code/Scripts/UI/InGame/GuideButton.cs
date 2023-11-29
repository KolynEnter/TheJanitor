using UnityEngine;

namespace CS576.Janitor.UI
{
    public class GuideButton : MonoBehaviour
    {
        [SerializeField]
        private GameMenuController _controller;

        public void GuideButtonPressed()
        {
            _controller.Switch(MenuType.Guide);
        }

        public void CloseGuidePanel()
        {
            _controller.HideAllPanels();
        }
    }
}
