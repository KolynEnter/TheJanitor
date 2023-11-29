using UnityEngine;


/*
    A scriptable object used to store a single integer
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/IntVariable", order = 1)]
    public class IntVariable : ScriptableObject
    {
        public int value;
    }
}
