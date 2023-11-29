using UnityEngine;


/*
    A scriptable object used to store the current minute and current second
    in game
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/GameTimeVariable", order = 0)]
    public class GameTimeVariable : ScriptableObject
    {
        public int gameMinuteTimer;
        public float gameSecondTimer;
    }
}
