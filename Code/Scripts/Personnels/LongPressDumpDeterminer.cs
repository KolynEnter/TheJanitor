using UnityEngine;

/*
    can dump the trash by long pressing the trash can
    the first 3 trash will be dumpped at a rate of 0.5s / trash
    after that, the rate will be 0.1s / trash
    disengaging from current press will reset rate
*/

namespace CS576.Janitor.Character
{
    public class LongPressDumpDeterminer
    {
        private readonly float r_slowDumpTime = 0.5f;

        private readonly float r_fastDumpTime = 0.2f;

        private readonly int r_fastThreshold = 3;

        private int _currentFastCount = 0;

        private float _timer = 0f;

        public bool IsLongPressDumpOK()
        {
            if (Input.GetMouseButton(0))
            {
                if (_currentFastCount < r_fastThreshold) // slow mode
                {
                    if (_timer > 0)
                    {
                        _timer -= Time.deltaTime;
                        return false;
                    }
                    _timer = r_slowDumpTime;
                    _currentFastCount += 1;
                    return true;
                }
                else // fast mode
                {
                    if (_timer > 0)
                    {
                        _timer -= Time.deltaTime;
                        return false;
                    }
                    _timer = r_fastDumpTime;
                    return true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currentFastCount = 0;
                _timer = 0f;
            }

            return false;
        }
    }
}
