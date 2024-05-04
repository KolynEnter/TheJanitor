using CS576.Janitor.Process;
using CS576.Janitor.Prop;
using CS576.Janitor.UI;
using UnityEngine;


/*
    Tracks the jammer's quantity
*/
namespace CS576.Janitor.Tools
{
    public class JammerTracker : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _jammerQuantity;

        [SerializeField]
        private ToolSwitcherUI _toolSwitcherUI;

        [SerializeField]
        private ToolStat _jammerStat;

        [SerializeField]
        private TrashBag _trashBag;

        public bool HasJammer
        {
            get { return _jammerQuantity.value > 0; }
        }

        // Increase the carrying weight when a new jammer is added
        public void IncrementJammerQuantity()
        {
            _jammerQuantity.value++;
            _trashBag.AddToolsWeight(_jammerStat.GetToolWeight);
            _toolSwitcherUI.ShowJammerQuantityLabel();
        }

        public void DecrementJammerQuantity()
        {
            _jammerQuantity.value--;
            _trashBag.AddToolsWeight(-_jammerStat.GetToolWeight);
            _toolSwitcherUI.ShowJammerQuantityLabel();
        }
    }
}
