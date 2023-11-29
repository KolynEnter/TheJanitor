using UnityEngine;
using System.Linq;
using CS576.Janitor.Tools;

namespace CS576.Janitor.Character
{
    public class HoldingTool : MonoBehaviour
    {
        [SerializeField]
        private GameObject _rightHandItemHolder;

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
    }
}
