using System;

public class TimerService
{
    // базовое событие: сколько осталось и какая длительность
    public event Action<float, float> TimeChanged;
    public event Action Finished;

    private float _duration;
    private float _timeLeft;

    private bool _isRunning;
    public bool IsRunning => _isRunning;

    public float Duration => _duration;
    public float TimeLeft => _timeLeft;

    public void Start(float seconds)
    {
        if (seconds <= 0f)
            return;

        _duration = seconds;
        _timeLeft = seconds;
        _isRunning = true;

        SendTimeChanged();
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void Resume()
    {
        if (_timeLeft > 0f)
            _isRunning = true;
    }

    public void Reset()
    {
        _timeLeft = _duration;
        SendTimeChanged();
    }

    public void Tick(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _timeLeft -= deltaTime;

        if (_timeLeft <= 0f)
        {
            _timeLeft = 0f;
            _isRunning = false;

            SendTimeChanged();
            Finished?.Invoke();
            return;
        }

        SendTimeChanged();
    }

    private void SendTimeChanged()
    {
        TimeChanged?.Invoke(_timeLeft, _duration);
    }
}