using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CS576.Janitor.Process;
using TMPro;


/*
    UI components located on the upper right corner
    of the game scene. It contains three sprites
    for the player's currently carrying tools.
    The one player is holding will be put in the center
    The player can also switch the current to the
    previous one or the next one. 

    Key pressing is not handled here but from 
    other scripts.

    Keep track of jammer's index in tool kit
*/
namespace CS576.Janitor.UI
{
    public class ToolSwitcherUI : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _currentToolImage;

        [SerializeField]
        private SpriteRenderer _prevToolImage;

        [SerializeField]
        private SpriteRenderer _nextToolImage;

        [SerializeField]
        private TextMeshProUGUI _currentToolQuantityLabel;

        [SerializeField]
        private TextMeshProUGUI _prevToolQuantityLabel;

        [SerializeField]
        private TextMeshProUGUI _nextToolQuantityLabel;

        [SerializeField]
        private ToolWithImage[] _allToolWithImages;

        [SerializeField]
        private IntVariable _jammerQuantity;

        private ToolWithImage[] _toolWithImages;

        private Dictionary<ToolType, int> _toolWithIndex = new Dictionary<ToolType, int>();

        private int _jammerIndex; // keep track of the index of the jammer in tool kit

        public void Initialize(ToolType[] includedToolTypes)
        {
            _currentToolQuantityLabel.enabled = false;
            _prevToolQuantityLabel.enabled = false;
            _nextToolQuantityLabel.enabled = false;

            ToolType tool1 = includedToolTypes[0];
            ToolType tool2 = includedToolTypes[1];

            _toolWithImages = _allToolWithImages.Where(x => x.toolType == ToolType.Hand || 
                    x.toolType == tool1 || 
                    x.toolType == tool2).ToArray();

            _toolWithIndex.Clear();
            for (int i = 0; i < _toolWithImages.Length; i++)
            {
                if (_toolWithImages[i].toolType == ToolType.Hand ||
                    _toolWithImages[i].toolType == tool1 || 
                    _toolWithImages[i].toolType == tool2)
                _toolWithIndex.Add(_toolWithImages[i].toolType, i);
            }

            _jammerIndex = _toolWithIndex.Count-1;
        }

        public void SwitchTo(ToolType currentHoldingTool)
        {
            int currentToolIndex = _toolWithIndex[currentHoldingTool];
            int newPrevIndex = GetPrevIndex(currentToolIndex);
            int newNextIndex = GetNextIndex(currentToolIndex);

            _currentToolImage.sprite = _toolWithImages[currentToolIndex].image;
            _prevToolImage.sprite = _toolWithImages[newPrevIndex].image;
            _nextToolImage.sprite = _toolWithImages[newNextIndex].image;

            ShowJammerQuantityLabel();
        }

        /* 
            If 'isPrev' is true, then switch to the previous tool
            Otherwise, the next tool
        */
        public void SwitchTool(ToolType currentHoldingTool, bool isPrev)
        {
            int currentToolIndex = _toolWithIndex[currentHoldingTool];

            int newPrevIndex;
            int newNextIndex;

            if (isPrev)
            {
                newNextIndex = currentToolIndex;
                currentToolIndex = GetPrevIndex(currentToolIndex);
                newPrevIndex = GetPrevIndex(currentToolIndex);
            }
            else
            {
                newPrevIndex = currentToolIndex;
                currentToolIndex = GetNextIndex(currentToolIndex);
                newNextIndex = GetNextIndex(currentToolIndex);
            }
            _currentToolImage.sprite = _toolWithImages[currentToolIndex].image;
            _prevToolImage.sprite = _toolWithImages[newPrevIndex].image;
            _nextToolImage.sprite = _toolWithImages[newNextIndex].image;

            UpdateJammerIndex(isPrev);
            ShowJammerQuantityLabel();
        }

        private int GetPrevIndex(int currIndex)
        {
            return currIndex - 1 < 0 ? _toolWithIndex.Count-1 : currIndex - 1;
        }

        private int GetNextIndex(int currIndex)
        {
            return currIndex + 1 > _toolWithIndex.Count-1 ? 0 : currIndex + 1;
        }

        /*
            Since this class doesn't actually have the order of tools
            It must track the order as tool is being switched each time
        */
        private void UpdateJammerIndex(bool isPrev)
        {
            if (isPrev)
            {
                if (_jammerIndex + 1 > _toolWithIndex.Count-1) 
                {
                    _jammerIndex = 0;
                }
                else
                {
                    _jammerIndex += 1;
                }
            }
            else
            {
                if (_jammerIndex - 1 < 0) 
                {
                    _jammerIndex = _toolWithIndex.Count-1;
                }
                else
                {
                    _jammerIndex -= 1;
                }
            }
        }

        public void ShowJammerQuantityLabel()
        {
            TextMeshProUGUI[] quantityLabels = new TextMeshProUGUI[3]
            {
                _prevToolQuantityLabel,
                _currentToolQuantityLabel,
                _nextToolQuantityLabel
            };

            foreach (TextMeshProUGUI quantityLabel in quantityLabels)
            {
                quantityLabel.enabled = false;
            }

            quantityLabels[_jammerIndex].enabled = true;
            quantityLabels[_jammerIndex].text = _jammerQuantity.value.ToString();
        }
    }
}
