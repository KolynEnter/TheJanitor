using CS576.Janitor.Process;
using UnityEngine;


/*
    Used to store a tool type and its sprite
*/
namespace CS576.Janitor.UI
{
    [System.Serializable]
    public struct  ToolWithImage
    {
        public ToolType toolType;
        public Sprite image;
    }
}
