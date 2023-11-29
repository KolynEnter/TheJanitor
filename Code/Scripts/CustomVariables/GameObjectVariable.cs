using UnityEngine;


/*
    A scriptable object used to store a single GameObject
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/GOVariable", order = 0)]
    public class GameObjectVariable : ScriptableObject
    {
        public GameObject value;
    }
}
