using UnityEngine;
using CS576.Janitor.Process;
using TMPro;

/*
    One of the "evil classes". Handles not only the animation
    but also the logic after it
*/

namespace CS576.Janitor.UI
{
    public class PanelShift : MonoBehaviour
    {
        [SerializeField]
        private float _initialPositionX;

        [SerializeField]
        private float _stopPositionX;

        [SerializeField]
        private float _endPositionX;

        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private GameObject _componentsHolder;

        [SerializeField]
        private GameSetter _setter;

        [SerializeField]
        private ClassicResultPanel _classicResultPanel; // lose the game

        [SerializeField]
        private ScoreResultPanel _scoreResultPanel; // show the score

        private float _currentPositionX; 

        private bool _isTriggered = false;

        private bool _hasPassedStopPhase = false;

        private float _t = 0.0f;

        private Timer _timer = new Timer(1.0f);

        // set to true once the entire animation is done
        private bool _hasBeenPerformed = false;

        private void Awake()
        {
            _isTriggered = false;
            _hasPassedStopPhase = false;
            _hasBeenPerformed = false;
            _t = 0.0f;
            _timer.Reset();
        }

        private void Start()
        {
            _componentsHolder.SetActive(false);
            _currentPositionX = _initialPositionX;
            _rectTransform.localPosition = new Vector3(_initialPositionX, 0.0f, 0.0f);
        }

        private void Update()
        {
            if (!_isTriggered)
                return;
            
            if (!_hasPassedStopPhase)
            {
                _t += Time.deltaTime;
                _currentPositionX = Mathf.Lerp(_initialPositionX, _stopPositionX, _t);

                if (_currentPositionX == _stopPositionX)
                {
                    _t = 0.0f;
                    _hasPassedStopPhase = true;
                }
            }
            else if (!_hasBeenPerformed)
            {
                // Stopping...
                if (!_timer.IsTimeOut())
                {
                    _timer.ElapseTime();
                    return;
                }
                _t += Time.deltaTime;
                _currentPositionX = Mathf.Lerp(_stopPositionX, _endPositionX, _t);

                if (_currentPositionX == _endPositionX)
                {
                    _hasBeenPerformed = true;

                    // Time's up!
                    if (_setter.GetGameLevel.GetMode == GameMode.Classic)
                    {
                        Debug.Log("Show the classic result panel");
                        _classicResultPanel.ShowPanel();
                    }
                    else
                    {
                        _scoreResultPanel.ShowPanel();
                    }
                    return;
                }
            }

            _rectTransform.localPosition = new Vector3(_currentPositionX, 0.0f, 0.0f);
        }

        public void TriggerShift()
        {
            _isTriggered = true;
            _componentsHolder.SetActive(true);
        }
    }
}
