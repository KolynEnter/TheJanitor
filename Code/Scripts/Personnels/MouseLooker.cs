using UnityEngine;


/*
    Use mouse to move the camera viewpoint
    Press 'Esc' key to exit focus
    Clicking on screen will enter focus
    (same as assignment 5)
*/
namespace CS576.Janitor.Character
{
    public class MouseLooker : MonoBehaviour
    {
        [SerializeField]
        private float _mouseSensivity = 100f;

        [SerializeField]
        private Transform _playerBody;

        [SerializeField]
        private float _minRotationX = -70f;

        [SerializeField]
        private float _maxRotationX = 70f;

        private float _xRotation = 0f;
        private bool _isDisabled = false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        /*
            Used to control the camera with mouse movement
            Code I discovered in my early learning stage
            This is the video:
            https://m.youtube.com/watch?v=b1uoLBp2I1w
        */
        private void Update()
        {
            if (_isDisabled)
                return;

            float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, _minRotationX, _maxRotationX);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }

        public void SetCameraLookDisable()
        {
            _isDisabled = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void SetCameraLookEnable()
        {
            _isDisabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
