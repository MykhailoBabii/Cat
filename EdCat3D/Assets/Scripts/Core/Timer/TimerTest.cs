using Core.Utilities
;
using TMPro;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private TextMeshProUGUI _timerText;

    private ITimer _timer;
    private void Start()
    {
        _timer = TimerService.CreateTimer(_duration, TimerDirection.Backward, OnTimerUpdate, OnTimerEnd);
        _timerText.SetText(_duration.ToString());
        _timer.Start();
    }

    private void OnTimerUpdate(float time)
    {
        _timerText.SetText(_timer.Current.ToString());
        //Debug.Log($"Timer: {_timer.Progress}");
    }

    private void OnTimerEnd()
    {
        Debug.Log($"Timer with duration:{_timer.Duration} complete");
    }
}