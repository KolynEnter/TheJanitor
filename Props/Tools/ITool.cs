using UnityEngine;
using CS576.Janitor.Trashes;

namespace CS576.Janitor.Tools
{
    public interface ITool
    {
        Process.ToolType GetToolType
        {
            get;
        }

        void Grab(GameObject gameObj);

#nullable enable
        Trash? GetOnHoldingTrash
        {
            get;
        }
#nullable disable

        void DestroyGrabbingTrash();
    }
}
