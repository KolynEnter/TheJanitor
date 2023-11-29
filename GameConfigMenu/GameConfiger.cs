using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


namespace CS576.Janitor.Process
{
    public class GameConfiger : MonoBehaviour
    {
        [SerializeField]
        private GameConfiguration _config;

        [SerializeField]
        private Button _modeButton;

        [SerializeField]
        private Button _difficultyButton;

        [SerializeField]
        private Button _tool1Button;

        [SerializeField]
        private GameObject _tool2ButtonObject;

        private Button _tool2Button;

        private string[] _modeArray;

        private string[] _difficultyArray;

        private string[] _toolArray;

        private void Awake()
        {
            _modeArray = Enum.GetNames(typeof(GameMode));
            _difficultyArray = Enum.GetNames(typeof(GameDifficulty));
            string[] allToolStrings = Enum.GetNames(typeof(ToolType));
            _toolArray = new string[allToolStrings.Length-2];
            int counter = 0;
            for (int i = 0; i < allToolStrings.Length; i++)
            {
                if (allToolStrings[i] != "Hand" && allToolStrings[i] != "Jammer")
                {
                    _toolArray[counter] = allToolStrings[i];
                    counter++;
                }
            }
        }

        private void Start() 
        {
            _tool2Button = _tool2ButtonObject.GetComponent<Button>();
            if (_tool2Button == null)
                throw new NullReferenceException("There is no tool 2 button.");
            
            _config.Defaultize();

            ChangeButtonText(_modeButton, _config.gameMode.ToString());
            ChangeButtonText(_difficultyButton, _config.gameDifficulty.ToString());
            ChangeButtonText(_tool1Button, _config.Tool1.ToString());
            ChangeButtonText(_tool2Button, _config.Tool2.ToString());

            if (_config.gameMode == GameMode.Invasion)
                _tool2ButtonObject.SetActive(false);
        }

        public void PressModeButton()
        {
            int nextIndex = PressButton(_modeButton, _modeArray);
            if (Enum.IsDefined(typeof(GameMode), nextIndex))
            {
                _config.gameMode = (GameMode) nextIndex;
            }
        }

        public void PressDifficultyButton()
        {
            int nextIndex = PressButton(_difficultyButton, _difficultyArray);
            if (Enum.IsDefined(typeof(GameDifficulty), nextIndex))
            {
                _config.gameDifficulty = (GameDifficulty) nextIndex;
            }
        }

        public void PressTool1Button()
        {
            int nextIndex = PressButton(_tool1Button, _toolArray);
            
            if (Enum.IsDefined(typeof(ToolType), nextIndex))
            {
                _config.Tool1 = (ToolType) nextIndex;
            }
            if (_config.Tool1 == _config.Tool2)
            {
                int nextnextIndex = PressButton(_tool1Button, _toolArray);
                if (Enum.IsDefined(typeof(ToolType), nextnextIndex))
                {
                    _config.Tool1 = (ToolType) nextnextIndex;
                }
            }
            
        }

        public void PressTool2Button()
        {
            int nextIndex = PressButton(_tool2Button, _toolArray);
            
            if (Enum.IsDefined(typeof(ToolType), nextIndex))
            {
                _config.Tool2 = (ToolType) nextIndex;
            }
            if (_config.Tool2 == _config.Tool1)
            {
                int nextnextIndex = PressButton(_tool2Button, _toolArray);
                if (Enum.IsDefined(typeof(ToolType), nextnextIndex))
                {
                    _config.Tool2 = (ToolType) nextnextIndex;
                }
            }
            
        }

        // returns the circular next index found
        private int PressButton(Button button, string[] enumArray)
        {
#nullable enable
            string? buttonText = GetButtonText(button);
#nullable disable
            if (buttonText == null)
                return -1;
            string _buttonText = (string) buttonText;
            int nextIndex = FindCircularNextIndexOf(_buttonText, enumArray);
            ChangeButtonText(button, enumArray[nextIndex]);

            return nextIndex;
        }

        private int FindCircularNextIndexOf<T>(T element, T[] array) where T : IComparable<T>
        {
            int currIndex = FindIndexOf(element, array);
            if (currIndex == -1)
                return -1;
            
            if (currIndex + 1 < array.Length)
            {
                return currIndex + 1;
            }
            else
            {
                return 0;
            }
        }

        private int FindIndexOf<T>(T element, T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (element.CompareTo(array[i]) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

#nullable enable
        private string? GetButtonText(Button button)
        {
            Transform textTransform = button.transform.GetChild(0);
            TextMeshProUGUI textComp = textTransform.GetComponent<TextMeshProUGUI>();
            if (textComp == null)
                return null;
            
            return textComp.text;
        }
#nullable disable

        private void ChangeButtonText(Button button, string newText)
        {
            Transform textTransform = button.transform.GetChild(0);
            TextMeshProUGUI textComp = textTransform.GetComponent<TextMeshProUGUI>();
            if (textComp == null)
                return;
            
            textComp.text = newText;
        }
    }
}
