using UnityEngine;


/*
    Used in the main scene
    switch between the main UI compnents
    and the configuration UI components
*/
namespace CS576.Janitor.UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Canvas _mainCanvas;

        [SerializeField]
        private Canvas _configCanvas;

        // used in onClick() of game configuration's 
        // close button
        public void SwitchToMain()
        {
            _mainCanvas.gameObject.SetActive(true);
            _configCanvas.gameObject.SetActive(false);
        }

        // used in onClick() of main's
        // 'start' button
        public void SwtichToConfig()
        {
            _mainCanvas.gameObject.SetActive(false);
            _configCanvas.gameObject.SetActive(true);
        }
    }
}
