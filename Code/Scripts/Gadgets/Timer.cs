using UnityEngine;


/*
    A little timer I write for almost every project of mine
    It counts down the time in the deltaTime scale

    Notice that the timer is initially 0.
*/
namespace CS576.Janitor.Process
{
    public struct Timer
    {
        private float _currentTime;

        private float _period;

        private float? _elapse;

        public Timer(float period, float? elapse=null)
        {
            _period = period;
            _currentTime = 0.0f;
            if (elapse != null)
            {
                _elapse = elapse;
            }
            else
            {
                _elapse = null;
            }
        }

        public bool IsTimeOut()
        {
            return _currentTime <= 0.0f;
        }

        public void ElapseTime()
        {
            if (_elapse != null)
            {
                _currentTime -= (float) _elapse;
            }
            else
            {
                _currentTime -= Time.deltaTime;
            }
        }

        public void Reset()
        {
            _currentTime = _period;
        }
    }
}
