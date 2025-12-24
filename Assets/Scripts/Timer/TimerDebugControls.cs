using UnityEngine;

public class TimerDebugControls : MonoBehaviour
{
    [SerializeField] private float _seconds = 10f;

    private TimerService _timer;

    public void Construct(TimerService timer)
    {
        _timer = timer;
    }

    private void Update()
    {
        if (_timer == null)
            return;

        if (Input.GetKeyDown(KeyCode.T))
            _timer.Start(_seconds);

        if (Input.GetKeyDown(KeyCode.R))
            _timer.Reset();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_timer.IsRunning)
                _timer.Stop();
            else
                _timer.Resume();
        }
    }
}