using UnityEngine;
using CS576.Janitor.Process;


/*
    The general game result panel shown
    after the player has won/lost in game mode
*/
namespace CS576.Janitor.UI
{
    public class GameResultPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _componentsHolder;

        [SerializeField]
        private GameEvent _hideAllPanels;

        public bool IsShown
        {
            get { return _componentsHolder.activeSelf; }
        }

        private void Start()
        {
            HidePanel();
        }

        public virtual void ShowPanel()
        {
            _hideAllPanels.TriggerEvent();
            _componentsHolder.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }

        private void HidePanel()
        {
            _componentsHolder.SetActive(false);
        }
    }
}
