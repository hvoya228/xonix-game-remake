using UnityEngine;

namespace Assets.Scripts
{
    public class Timer
    {
        private float _time;
        private bool _timeIsUp;

        public float Seconds { get { return _time; } }
        public bool TimeIsUp { get { return _timeIsUp; } }

        public void SetTimer()
        {
            _timeIsUp = false;
            _time = 60;
        }

        public void IncreasingTime()
        {
            if (PlayMode.gameOver.IsGameOver || PlayMode.gameOver.IsPaused)
            {
                return;
            }

            if (_time > 0)
            {
                _time -= Time.deltaTime;
            }
            else
            {
                _timeIsUp = true;
            }
        }
    }
}