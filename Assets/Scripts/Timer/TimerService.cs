using System;

public class TimerService
{
    public event Action<float> NormalizedChanged; //0,,1
    public event Action<int> SecondsLeftChanged; //для сердечек
    public event Action Finished;

    private float _duration;
    private float _timeLeft;
    
    private bool _isRunning;
    public bool IsRunning => _isRunning;
   

    private int _lastSeconds;

    public void Start(float seconds)
    {
        _duration = seconds;
        _timeLeft = seconds;
        _isRunning = true;

        _lastSeconds = GetSecondsLeft();
        SendAll();
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
        _lastSeconds = GetSecondsLeft();
        SendAll();
    }

    public void Tick(float deltaTime)
    {
        if (!_isRunning)
            return;

        _timeLeft -= deltaTime;

        if (_timeLeft <= 0f)
        {
            _timeLeft = 0f;
            _isRunning = false;
            
            SendAll();
            Finished?.Invoke();
            return;
        }

        SendChanged();
    }

    private void SendAll()
    {
        NormalizedChanged?.Invoke(GetNormalized());
        SecondsLeftChanged?.Invoke(GetSecondsLeft());
    }

    private void SendChanged()
    {
        NormalizedChanged?.Invoke(GetNormalized());

        var seconds = GetSecondsLeft();
        if (seconds != _lastSeconds)
        {
            _lastSeconds = seconds;
            SecondsLeftChanged?.Invoke(seconds);
        }
    }

    private float GetNormalized()
    {
        return _duration <= 0f ? 0f : _timeLeft / _duration; // убыввает 1>0
    }

    private int GetSecondsLeft()
    {
        return (int)Math.Ceiling(_timeLeft);
    }
}