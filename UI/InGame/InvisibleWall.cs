using UnityEngine;


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