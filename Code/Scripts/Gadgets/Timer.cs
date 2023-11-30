using UnityEngine;


/*
    A little timer I write for almost every project of mine
    It counts down the time in the deltaTime scale
*/
namespace CS576.Janitor.Process
{
    public struct Timer
    {
        private float _currentTime;

        private float _period;

        public Timer(float period)
        {
            _period = period;
            _currentTime = 0.0f;
        }

        public bool IsTimeOut()
        {
            return _currentTime <= 0.0f;
        }

        public void ElapseTime()
        {
            _currentTime -= Time.deltaTime;
        }

        public void Reset()
        {
            _currentTime = _period;
        }
    }
}
