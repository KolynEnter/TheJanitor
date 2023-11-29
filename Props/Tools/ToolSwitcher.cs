using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CS576.Janitor.UI;
using CS576.Janitor.Character;
using CS576.Janitor.Process;


namespace CS576.Janitor.Tools
{
    public class ToolSwitcher : MonoBehaviour
    {
        [SerializeField]
        private ToolSwitcherUI _toolSwitcherUI;

        [SerializeField]
        private HoldingTool _holdingTool;

        [SerializeField]
        private BaseTool[] _allTools;

        private BaseTool[] _tools;

        [SerializeField]
        private GameSetter _gameSetting;

        private Dictionary<ToolType, int> _toolWithIndex = new Dictionary<ToolType, int>();

        private Timer _timer = new Timer(0.3f);

        private void Start()
        {
            ToolType tool1 = _gameSetting.GetConfig.Tool1;
            ToolType tool2 = _gameSetting.GetConfig.Tool2;

            _tools = _allTools.Where(x => x.GetToolType == ToolType.Hand || 
                    x.GetToolType == tool1 || 
                    x.GetToolType == tool2).ToArray();
            
            _toolWithIndex.Clear();
            for (int i = 0; i < _tools.Length; i++)
            {
                _toolWithIndex.Add(_tools[i].GetToolType, i);
            }
            _toolSwitcherUI.Initialize(new ToolType[2] {tool1, tool2});
            _toolSwitcherUI.SwitchTo(_holdingTool.GetTool.GetToolType);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.J))
            {
                if (!_timer.IsTimeOut())
                    return;
                _timer.Reset();
                int currentIndex = _toolWithIndex[_holdingTool.GetTool.GetToolType];
                _tools[currentIndex].OnSwitchToOtherTool();

                _toolSwitcherUI.SwitchTool(_holdingTool.GetTool.GetToolType, true);
                int newCurrentIndex = GetPrevIndex(currentIndex);
                DisableAllTools();
                _tools[newCurrentIndex].gameObject.SetActive(true);
                _tools[newCurrentIndex].OnSwitchToThisTool();
            } 
            if (Input.GetKey(KeyCode.L))
            {
                if (!_timer.IsTimeOut())
                    return;
                _timer.Reset();
                int currentIndex = _toolWithIndex[_holdingTool.GetTool.GetToolType];
                _tools[currentIndex].OnSwitchToOtherTool();

                _toolSwitcherUI.SwitchTool(_holdingTool.GetTool.GetToolType, false);
                int newCurrentIndex = GetNextIndex(currentIndex);
                DisableAllTools();
                _tools[newCurrentIndex].gameObject.SetActive(true);
                _tools[newCurrentIndex].OnSwitchToThisTool();
            }
            _timer.ElapseTime();
        }

        private void DisableAllTools()
        {
            foreach (BaseTool tool in _tools)
            {
                tool.gameObject.SetActive(false);
            }
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
