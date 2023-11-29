using UnityEngine;
using UnityEngine.SceneManagement;


namespace CS576.Janitor.UI
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField]
        private GameMenuController _controller;

        public void HomeButtonPressed()
        {
            _controller.Switch(MenuType.Home);
        }

        public void CloseHomePanel()
        {
            _controller.HideAllPanels();
        }

        public void GoBackToHome()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
