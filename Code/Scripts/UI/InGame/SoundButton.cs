using UnityEngine;
using UnityEngine.UI;


/*
    The sound button located in the game mode
    on the right side of the UI
*/
namespace CS576.Janitor.UI
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField]
        private Scrollbar _musicScrollbar;

        [SerializeField]
        private Scrollbar _sfxScrollbar;

        [SerializeField]
        private GameMenuController _controller;

        public void Start()
        {
            _musicScrollbar.value = AudioManager.Instance.GetMusicVolume;
            _sfxScrollbar.value = AudioManager.Instance.GetSFXVolume;
        }

        public void AdjustMusicValue()
        {
            AudioManager.Instance.AdjustMusicVolume(_musicScrollbar.value);
        }

        public void AdjustSFXValue()
        {
            AudioManager.Instance.AdjustSFXVolume(_sfxScrollbar.value);
        }

        public void SoundButtonPressed()
        {
            _controller.Switch(MenuType.Sound);
        }

        public void CloseSoundPanel()
        {
            _controller.HideAllPanels();
        }
    }
}
