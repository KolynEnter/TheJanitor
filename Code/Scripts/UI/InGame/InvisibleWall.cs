using UnityEngine;


/*
    Attached to the white walls in the game scene
    In the beginning of the game, hide the wall's
    MeshRenderer, thus making it look invisible
*/
namespace CS576.Janitor.UI
{
    public class InvisibleWall : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer;

        private void Start()
        {
            _renderer.enabled = false;
        }
    }
}