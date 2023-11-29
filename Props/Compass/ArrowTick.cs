using UnityEngine;


namespace CS576.Janitor.Process
{
    public class ArrowTick : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        private Vector3 _startPosition;
        private Vector3 _shiftPosition;
        [SerializeField] private bool _movingFoward = true;

        private void Start()
        {
            _startPosition = transform.localPosition;
            _shiftPosition = _startPosition + Vector3.forward * _distance;
        }

        private void Update()
        {
            float step = Mathf.Sin(_speed * Time.deltaTime);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,
                                                    _movingFoward ? 
                                                    _shiftPosition :
                                                    _startPosition,
                                                    step);
            if (_movingFoward && transform.localPosition == _shiftPosition)
            {
                _movingFoward = false;
            }
            else if (!_movingFoward && transform.localPosition == _startPosition)
            {
                _movingFoward = true;
            }
        }
    }
}
