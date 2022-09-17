using System;
using System.Collections;
using UnityEngine;

namespace Core.Utilities

{
    public enum TimerDirection
    {
        Forward = 0,
        Backward
    }

    public interface ITimerInfo
    {
        long StartTime { get; }
        long EndTime { get; }
        float Progress { get; }
        float Current { get; }
        float Duration { get; }

        bool IsStarted { get; }
        bool IsPaused { get; }

        event Action<float> OnTimerUpdateEvent;
        event Action OnTimerEndEvent;
        event Action<bool> OnTimerPauseEvent;
    }

    public interface ITimer : ITimerInfo
    {
        void Pause();
        void UnPause();
        void Stop();
        void Start(bool useRealtime = false);
        void SetupNewTime(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1);

        void ContinueTime(DateTime startTime, DateTime endTime, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd);
    }

    public class Timer : ITimer
    {
        private bool _isTimerStarted;
        private bool _isPaused;
        private bool _isStopped;

        private long _startTime;
        private long _endTime;

        private TimerDirection _direction;

        private Action _onTimerEnd;
        private Action<float> _onUpdate;

        private float _duration;
        private float _current;

        public event Action<float> OnTimerUpdateEvent;
        public event Action OnTimerEndEvent;
        public event Action<bool> OnTimerPauseEvent;

        public long StartTime => _startTime;

        public long EndTime => _endTime;

        public float Progress => _current / _duration;

        public float Current => _current;

        public float Duration => _duration;

        public bool IsStarted => _isTimerStarted;
        public bool IsPaused => _isPaused;

        private float _timeCounter;

        private float _speed;

        public Timer(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed)
        {
            _duration = duration;
            _direction = direction;
            _onUpdate = onUpdate;
            _onTimerEnd = onTimerEnd;
            _speed = speed;
        }

        public void SetupNewTime(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1)
        {
            _duration = duration;
            _direction = direction;
            _onUpdate = onUpdate;
            _onTimerEnd = onTimerEnd;
            _speed = speed;
        }

        public static Timer Empty()
        {
            return new Timer(0, TimerDirection.Backward, null, null, 1);
        }

        public void Pause()
        {
            _isPaused = true;
            OnTimerPauseEvent?.Invoke(_isPaused);
        }

        public void Start(bool useRealtime = false)
        {
            SetupStartData();
            //Debug.Log($"[Timer][Start] Start time {_duration}");
            if (useRealtime == false)
            {
                CoroutineManager.StartCoroutineMethod(TimerUpdateTimeDeltaTime());
            }
            else
            {
                CoroutineManager.StartCoroutineMethod(TimerUpdateRealtime());
            }
        }

        public void ContinueTime(DateTime startTime, DateTime endTime, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd)
        {
            SetupStartData();
            _startTime = startTime.Ticks;
            _endTime = endTime.Ticks;
            _direction = direction;
            _onUpdate = onUpdate;
            _onTimerEnd = onTimerEnd;
            CoroutineManager.StartCoroutineMethod(TimerUpdateRealtime());
        }

        public void Stop()
        {
            _isStopped = true;
            _isTimerStarted = false;
            CoroutineManager.StopCoroutineMethod(TimerUpdateTimeDeltaTime());
            CoroutineManager.StopCoroutineMethod(TimerUpdateRealtime());
        }

        public void UnPause()
        {
            _isPaused = false;
            OnTimerPauseEvent?.Invoke(_isPaused);
        }

        private void SetupStartData()
        {
            _timeCounter = 0;
            _startTime = DateTime.UtcNow.Ticks;
            _endTime = DateTime.UtcNow.AddSeconds(_duration).Ticks;
            _isStopped = false;
            _isPaused = false;
        }

        private IEnumerator TimerUpdateTimeDeltaTime()
        {
            //Debug.Log($"[Timer][TimerUpdate] Start time {_duration}");
            if (_isTimerStarted)
            {
                Debug.LogError($"[Timer][TimerUpdateTimeDeltaTime] error: time already started! {_duration}");
                yield break;
            }

            _isTimerStarted = true;
            while (_timeCounter < _duration)
            {
                if (_isStopped)
                    yield break;

                if (_isPaused)
                {
                    yield return null;
                }
                else
                {
                    if (_direction == TimerDirection.Forward)
                    {
                        _current = _timeCounter;//(float)(DateTime.Now.Ticks - _startTime) / 10000000;
                    }
                    else
                    {
                        _current = _duration - _timeCounter;//(float)(_endTime - DateTime.Now.Ticks) / 10000000;
                    }
                    _timeCounter += UnityEngine.Time.deltaTime * _speed;
                    _onUpdate?.Invoke(_current);
                    OnTimerUpdateEvent?.Invoke(_current);
                }
                yield return null;
            }

            if(_direction == TimerDirection.Forward)
            {
                _timeCounter = _duration;
                _current = _timeCounter;
            }
            else
            {
                _timeCounter = 0;
                _current = 0;
            }

            _isTimerStarted = false;
            _onTimerEnd?.Invoke();
            OnTimerEndEvent?.Invoke();
        }

        private IEnumerator TimerUpdateRealtime()
        {
            if (_isTimerStarted)
            {
                Debug.LogError($"[Timer][TimerUpdateRealtime] error: time already started! {_duration}");
                yield break;
            }

            _isTimerStarted = true;
            while (DateTime.UtcNow.Ticks < _endTime)
            {
                if (_isStopped)
                    yield break;

                if (_isPaused)
                {
                    yield return null;
                }
                else
                {
                    if (_direction == TimerDirection.Forward)
                    {
                        _current = (float)(DateTime.UtcNow.Ticks - _startTime) / 10000000;
                    }
                    else
                    {
                        _current = (float)(_endTime - DateTime.UtcNow.Ticks) / 10000000;
                    }

                    _onUpdate?.Invoke(_current);
                    OnTimerUpdateEvent?.Invoke(_current);
                }
                yield return null;
            }

            if (_direction == TimerDirection.Forward)
            {
                _current = _duration;
            }
            else
            {
                _current = 0;
            }

            _isTimerStarted = false;
            _onTimerEnd?.Invoke();
            OnTimerEndEvent?.Invoke();
        }

        
    }
}