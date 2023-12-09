using UnityEngine;
using CS576.Janitor.Prop;
using CS576.Janitor.Process;


/*
    Add the tools' weight to the trash bag
    in the beginning of the game

    The weight of jammers added later is not handled here but in the jammer tracker
*/
namespace CS576.Janitor.Tools
{
    public class ToolWeightAdder : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private GameObject _rightHandItemHolder;

        [SerializeField]
        private TrashBag _trashBag;

        [SerializeField]
        private ToolStat _jammerStat;

        public void Initialize(GameSetter gameSetter)
        {
            float totalToolsWeight = 0;
            ToolType tool1 = gameSetter.GetConfig.Tool1;
            ToolType tool2 = gameSetter.GetConfig.Tool2;

            for (int i = 0; i < _rightHandItemHolder.transform.childCount; i++)
            {
                Transform childTransform = _rightHandItemHolder.transform.GetChild(i);
                if (childTransform.gameObject.CompareTag("Grip"))
                    continue;
                
                BaseTool tool = childTransform.GetComponent<BaseTool>();
                if (tool != null)
                {
                    if (tool.GetToolType == tool1 || tool.GetToolType == tool2 && tool.GetToolType != ToolType.Jammer)
                        totalToolsWeight += tool.GetToolWeight;
                }
            }

            totalToolsWeight += _jammerStat.GetToolWeight * gameSetter.GetGameLevel.GetStartingJammerNumber;

            _trashBag.AssignToolsWeight(totalToolsWeight);
        }
    }
}
