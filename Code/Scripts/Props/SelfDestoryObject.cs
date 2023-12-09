using UnityEngine;


/*
    Destroy the host game object in the start of the game
*/
namespace CS576.Janitor.Prop
{
    public class SelfDestoryObject : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject);
        }
    }
}
