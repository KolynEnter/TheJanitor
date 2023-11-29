using UnityEngine;

namespace CS576.Janitor.UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Canvas _mainCanvas;

        [SerializeField]
        private Canvas _configCanvas;

        public void SwitchToMain()
        {
            _mainCanvas.gameObject.SetActive(true);
            _configCanvas.gameObject.SetActive(false);
        }

        public void SwtichToConfig()
        {
            _mainCanvas.gameObject.SetActive(false);
            _configCanvas.gameObject.SetActive(true);
        }
    }
}
