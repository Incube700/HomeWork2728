using UnityEngine;
using UnityEngine.UI;

public class TimerSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private TimerService _timer;

    public void Construct(TimerService timer)
    {
        _timer = timer;

        _timer.TimeChanged += OnTimeChanged;
        _timer.Finished += OnFinished;

        OnTimeChanged(_timer.TimeLeft, _timer.Duration);
    }

    private void OnDestroy()
    {
        if (_timer == null)
            return;

        _timer.TimeChanged -= OnTimeChanged;
        _timer.Finished -= OnFinished;
    }

    private void OnTimeChanged(float timeLeft, float duration)
    {
        float normalized = duration <= 0f ? 0f : timeLeft / duration;
        _slider.value = normalized;
    }

    private void OnFinished()
    {
        _slider.value = 0f;
    }
}