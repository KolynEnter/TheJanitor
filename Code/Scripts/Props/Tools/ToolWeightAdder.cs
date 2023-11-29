using UnityEngine;
using CS576.Janitor.Prop;
using CS576.Janitor.Process;


namespace CS576.Janitor.Tools
{
    public class ToolWeightAdder : MonoBehaviour
    {
        [SerializeField]
        private GameObject _rightHandItemHolder;

        [SerializeField]
        private TrashBag _trashBag;

        [SerializeField]
        private UI.CapacityUIManager _capUIManager;

        [SerializeField]
        private GameSetter _gameSetting;

        private void Start()
        {
            float totalToolsWeight = 0;
            ToolType tool1 = _gameSetting.GetConfig.Tool1;
            ToolType tool2 = _gameSetting.GetConfig.Tool2;

            for (int i = 0; i < _rightHandItemHolder.transform.childCount; i++)
            {
                Transform childTransform = _rightHandItemHolder.transform.GetChild(i);
                if (childTransform.gameObject.CompareTag("Grip"))
                    continue;
                
                BaseTool tool = childTransform.GetComponent<BaseTool>();
                if (tool != null)
                {
                    if (tool.GetToolType == tool1 || tool.GetToolType == tool2)
                        totalToolsWeight += tool.GetToolWeight;
                }
            }
            _trashBag.AssignToolsWeight(totalToolsWeight);

            _capUIManager.UpdateUI(_trashBag.GetCurrentWeight, _trashBag.GetTotalCapacity);
        }
    }
}
