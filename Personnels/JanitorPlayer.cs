using UnityEngine;

namespace CS576.Janitor.Character
{
    public class JanitorPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private CharacterController _controller;

        [SerializeField] private float _fullSpeed = 5.0f;
        [SerializeField] private float _turnSpeed = 100.0f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Prop.TrashBag _trashBag;

        private Process.Timer _runningTimer = new Process.Timer(1.0f);
        private Vector3 _lastPosition;
        private float _timeSinceLastMove = 0f;
        private float _idleTimeThreshold = 0.5f;
        private bool _isPlayerStuck = false;

        private Vector3 _moveDirection = Vector3.zero;

        private float GetCurrentSpeed
        {
            get
            {
                return _fullSpeed * 
                (1 - (_trashBag.GetCurrentWeight / (_trashBag.GetTotalCapacity * 1.65f)));
            }
        }

        private void Start()
        {
            _lastPosition = transform.position;
        }

        private void Update()
        {
            CheckStuck();
            ReadInput();

            if (_controller.isGrounded)
            {
                _moveDirection = transform.forward * Input.GetAxis("Vertical") * GetCurrentSpeed;
            }

            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * _turnSpeed * Time.deltaTime, 0);
            _controller.Move(_moveDirection * Time.deltaTime);
            _moveDirection.y += _gravity * Time.deltaTime;
        }

        private void CheckStuck()
        {
            if (HasPlayerMoved())
            {
                _timeSinceLastMove = 0f;
                _isPlayerStuck = false;
            }
            else
            {
                _timeSinceLastMove += Time.deltaTime;

                if (_timeSinceLastMove >= _idleTimeThreshold)
                {
                    _isPlayerStuck = true;
                }
            }
            _lastPosition = transform.position;
        }

        private void ReadInput()
        {
            if (Input.GetKey("w"))
            {
                _anim.SetBool("IsRunning", true);
            }
            else
            {
                _anim.SetBool("IsRunning", false);
            }

            if (Input.GetKey("s"))
            {
                _anim.SetBool("IsRunningBack", true);
            }
            else
            {
                _anim.SetBool("IsRunningBack", false);
            }

            if (Input.GetAxis("Vertical") != 0 && _runningTimer.IsTimeOut() && !_isPlayerStuck)
            {
                UI.AudioManager.Instance.PlaySFX(UI.GameSFX.Running);
                _runningTimer.Reset();
            }
            _runningTimer.ElapseTime();
        }

        private bool HasPlayerMoved()
        {
            return transform.position != _lastPosition;
        }
    }
}
