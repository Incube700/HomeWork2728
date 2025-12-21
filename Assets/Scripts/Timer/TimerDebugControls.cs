using UnityEngine;

public class TimerDebugControls : MonoBehaviour
{
    [SerializeField] private TimerRunner _runner;
    [SerializeField] private float _seconds = 10f;
    [SerializeField] private TimerHeartsView _heartsView;

    private TimerService _timer;
    
    private void Start()
    {
        if (_runner == null)
        {
            Debug.LogError("TimerDebugControls: Runner not assigned!");
            enabled = false;
            return;
        }

        _timer = _runner.Timer;

        if (_timer == null)
        {
            Debug.LogError("TimerDebugControls: Runner.Timer is null!");
            enabled = false;
            return;
        }

        _timer.SecondsLeftChanged += OnSecondsLeftChanged;
        _timer.Finished += OnFinished;
    }

    private void OnDestroy()
    {
        if (_timer == null)
            return;

        _timer.SecondsLeftChanged -= OnSecondsLeftChanged;
        _timer.Finished -= OnFinished;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _timer.Start(_seconds);

            if (_heartsView != null)
                _heartsView.BuildHearts((int)_seconds);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _timer.Reset();

            if (_heartsView != null)
                _heartsView.BuildHearts((int)_seconds);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_timer.IsRunning)
                _timer.Stop();
            else
                _timer.Resume();
        }
    }

    private void OnSecondsLeftChanged(int seconds)
    {
        Debug.Log("Осталось Секунд" + seconds);
    }

    private void OnFinished()
    {
        Debug.Log("Время закончилось");
    }
}