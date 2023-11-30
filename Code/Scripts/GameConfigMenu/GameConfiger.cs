using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


/*
    This class exists in the "game configuration page", you can go to
    this page by: Open the game -> Click 'Start game' button.

    This class controls the entire page.
*/
namespace CS576.Janitor.Process
{
    public class GameConfiger : MonoBehaviour
    {
        /*
            a SO passed into the game scene as well
            Variables in SO is perserved across scene
            which is perfect for game configuration
        */
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
            /*
                Use Enum.GetNames so that when I add a new item to enum, I do not need 
                to rememeber to add it to here
            */
            _modeArray = Enum.GetNames(typeof(GameMode));
            _difficultyArray = Enum.GetNames(typeof(GameDifficulty));
            string[] allToolStrings = Enum.GetNames(typeof(ToolType));

            // every tool except hand and jammer because they cannot be selected
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

        /*
            Initialize everything in the game configuration page
        */
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

        /*
            Called when the 'mode' button is pressed
            By default, it should be 'classic' mode.
            Click on it should change to 'score' mode, then again it changes
            to 'invasion' mode. Click again goes back to 'classic' mode.

            This applies to the proposal version
        */
        public void PressModeButton()
        {
            int nextIndex = PressButton(_modeButton, _modeArray);
            if (Enum.IsDefined(typeof(GameMode), nextIndex))
            {
                _config.gameMode = (GameMode) nextIndex;
            }
        }

        /*
            Called when the difficulty button is pressed
            By default, it should be 'normal' difficulty.
            Click on it should change to 'hard' difficulty, then again it
            changes back to 'normal' difficulty.

            This applies to the proposal version
        */
        public void PressDifficultyButton()
        {
            int nextIndex = PressButton(_difficultyButton, _difficultyArray);
            if (Enum.IsDefined(typeof(GameDifficulty), nextIndex))
            {
                _config.gameDifficulty = (GameDifficulty) nextIndex;
            }
        }

        /*
            Similar to the two buttons above.
            This one is for tools. Clicking on it will rotate the tools.

            However, Tool1 button cannot have the same tool as Tool2 button.
        */
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

        /*
            Exactly the same as Tool1 button. 

            Still, Tool1 button cannot have the same tool as Tool2 button.
        */
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

        /*
            Changes the text of the tool button label to the circular next tool

            returns the circular next index found
            If the array has a length of 5. YOu are in index 0,
                the next index should be 1
            If the array has a length of 5. You are in index 4,
                the next index should be 0.
        */
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

        /*
            returns the circular next index found
            If the array has a length of 5. YOu are in index 0,
                the next index should be 1
            If the array has a length of 5. You are in index 4,
                the next index should be 0.
        */
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

        // Get the text string in a button
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

        // Change the text in a button
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
