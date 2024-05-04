using UnityEngine;


/*
    The guide button located in the game mode
    on the right side of the UI
*/
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
