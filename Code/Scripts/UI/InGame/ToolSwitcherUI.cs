using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CS576.Janitor.Process;


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
        private ToolWithImage[] _allToolWithImages;

        private ToolWithImage[] _toolWithImages;

        private Dictionary<ToolType, int> _toolWithIndex = new Dictionary<ToolType, int>();

        public void Initialize(ToolType[] includedToolTypes)
        {
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
        }

        public void SwitchTo(ToolType currentHoldingTool)
        {
            int currentToolIndex = _toolWithIndex[currentHoldingTool];
            int newPrevIndex = GetPrevIndex(currentToolIndex);
            int newNextIndex = GetNextIndex(currentToolIndex);

            _currentToolImage.sprite = _toolWithImages[currentToolIndex].image;
            _prevToolImage.sprite = _toolWithImages[newPrevIndex].image;
            _nextToolImage.sprite = _toolWithImages[newNextIndex].image;
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
        }

        private int GetPrevIndex(int currIndex)
        {
            return currIndex - 1 < 0 ? _toolWithIndex.Count-1 : currIndex - 1;
        }

        private int GetNextIndex(int currIndex)
        {
            return currIndex + 1 > _toolWithIndex.Count-1 ? 0 : currIndex + 1;
        }
    }
}
