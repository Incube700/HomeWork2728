using UnityEngine;

public class TimerRunner : MonoBehaviour
{
    private TimerService _timer;

    public void Construct(TimerService timer)
    {
        _timer = timer;
    }

    private void Update()
    {
        if (_timer != null)
            _timer.Tick(Time.deltaTime);
    }
}