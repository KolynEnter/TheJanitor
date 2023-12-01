using UnityEngine;


/*
    A UI component with a fill bar and a pointer
    (the curosr)

    When the fillAmount is updated, the position
    of the cursor also updates
*/
namespace CS576.Janitor.UI
{
    public class BarPointerManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pointer;

        [SerializeField]
        private GameObject _pointerStart;

        [SerializeField]
        private GameObject _pointerEnd;

        private float _totalX;

        private float _startX;

        private Vector3 _baseVector;

        private void Start()
        {
            _totalX = _pointerEnd.GetComponent<RectTransform>().localPosition.x - _pointerStart.GetComponent<RectTransform>().localPosition.x;
            _startX = _pointerStart.GetComponent<RectTransform>().localPosition.x;
            if (_pointer.activeSelf)
            {
                _baseVector = new Vector3(_startX, 
                            _pointer.GetComponent<RectTransform>().localPosition.y, 
                            _pointer.GetComponent<RectTransform>().localPosition.z);
            }
        }

        public void UpdatePointerPosition(float xRatio)
        {
            float addition = xRatio * _totalX;
            if (_pointer.activeSelf)
            {
                _pointer.GetComponent<RectTransform>().localPosition = 
                    _baseVector + new Vector3(addition, 0f, 0f);
            }
        }
    }
}
