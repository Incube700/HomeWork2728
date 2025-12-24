using UnityEngine;

public class TimerEntryPoint : MonoBehaviour
{
    [SerializeField] private TimerRunner _runner;
    [SerializeField] private TimerSliderView _sliderView;
    [SerializeField] private TimerHeartsView _heartsView;
    [SerializeField] private TimerDebugControls _debugControls;

    private TimerService _timer;

    private void Awake()
    {
        _timer = new TimerService();

        if (_runner != null)
            _runner.Construct(_timer);

        if (_sliderView != null)
            _sliderView.Construct(_timer);

        if (_heartsView != null)
            _heartsView.Construct(_timer);

        if (_debugControls != null)
            _debugControls.Construct(_timer);
    }
}