using UnityEngine;


/*
    Rotates the host gameobject in the
    y axis forever
*/
namespace CS576.Janitor.UI
{
    public class RotateForever : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 5f;

        private void Awake()
        {
            Time.timeScale = 1.0f;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }
}
