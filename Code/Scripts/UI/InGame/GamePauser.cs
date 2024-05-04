using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
    Pause/uppause the game when the F key
    is pressed.
    Does not work if the game has been ended
*/
namespace CS576.Janitor.UI
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField]
        private Image _blackBG;

        [SerializeField]
        private TextMeshProUGUI _focusLabel;

        [SerializeField]
        private Sprite _pauseSourceSprite;

        [SerializeField]
        private Sprite _resumeSourceSprite;

        [SerializeField]
        private Image _pauseButtonImage;

        private bool _isPaused = false;

        [SerializeField]
        private GameMenuController _menuController;

        private void Start()
        {
            ResumeGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            if (_menuController.HasPanelShown)
                return;
            
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        private void PauseGame()
        {
            if (_menuController.HasAnyGameResultPanelShown)
                return;
            
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            _blackBG.enabled = true;
            _focusLabel.text = "MODE: FOCUS";
            _pauseButtonImage.sprite = _resumeSourceSprite;
        }

        private void ResumeGame()
        {
            if (_menuController.HasAnyGameResultPanelShown)
                return;

            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            _blackBG.enabled = false;
            _focusLabel.text = "MODE: NOT FOCUS";
            _pauseButtonImage.sprite = _pauseSourceSprite;
        }
    }
}
