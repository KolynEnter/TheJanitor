using UnityEngine;
using CS576.Janitor.Tools;


/*
    Determines the player's current holding (using) tool
*/
namespace CS576.Janitor.Character
{
    public class HoldingTool : MonoBehaviour
    {
        [SerializeField]
        private GameObject _rightHandItemHolder;

        [SerializeField]
        private GameObject _trashParent;

        public ITool GetTool
        {
            get
            {
                for (int i = 0; i < _rightHandItemHolder.transform.childCount; i++)
                {
                    Transform childTransform = _rightHandItemHolder.transform.GetChild(i);
                    if (!childTransform.gameObject.activeSelf)
                        continue;

                    if (childTransform.gameObject.CompareTag("Grip"))
                        continue;
                    
                    ITool tool = childTransform.GetComponent(typeof (ITool)) as ITool;
                    if (tool != null)
                        return tool;
                }

                return null;
            }
        }

        public bool HasTrashOnCurrentHoldingTool
        {
            get
            {
                return _trashParent.transform.childCount != 0;
            }
        }
    }
}
