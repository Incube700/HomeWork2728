using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHeartsView : MonoBehaviour
{
    [SerializeField] private TimerRunner _runner;
    [SerializeField] private Transform _container;
    [SerializeField] private Image _heartPrefab;

    private TimerService _timer;
    private readonly List<Image> _hearts = new List<Image>();

    private void Start()
    {
        _timer = _runner.Timer;

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

    public void BuildHearts(int count)
    {
        ClearHearts();

        for (int i = 0; i < count; i++)
        {
            Image heart = Instantiate(_heartPrefab, _container);
            _hearts.Add(heart);
        }

        SetVisible(count);
    }

    private void OnSecondsLeftChanged(int secondsLeft)
    {
        if (_hearts.Count == 0)
            BuildHearts(secondsLeft);

        SetVisible(secondsLeft);
        
        Debug.Log("Hearts secondsLeft =" + secondsLeft);
    }

    private void OnFinished()
    {
        SetVisible(0);
    }

    private void SetVisible(int secondsLeft)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].gameObject.SetActive(i < secondsLeft);
        }
    }

    private void ClearHearts()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            Destroy(_hearts[i].gameObject);
        }

        _hearts.Clear();
    }
}