using System;

namespace Core.Utilities

{
    public static class TimerService
    {
        public static ITimer CreateTimer(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1)
        {
            return new Timer(duration, direction, onUpdate, onTimerEnd, speed);
        }

        public static ITimer CreateTimer(float duration, TimerDirection direction, float speed = 1)
        {
            return new Timer(duration, direction, null, null, speed);
        }
    }
}