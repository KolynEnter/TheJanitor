using UnityEngine;
using CS576.Janitor.Trashes;


/*
    The interface of a tool
    a tool must have a tool type, can grab trash
*/
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
        // This only applies to trash that will remain on hand
        // until disposed, i.e. Heavy trash bag
        Trash? GetOnHoldingTrash
        {
            get;
        }
#nullable disable

        // This only applies to trash that will remain on hand
        // until disposed, i.e. Heavy trash bag
        void DestroyGrabbingTrash();
    }
}
