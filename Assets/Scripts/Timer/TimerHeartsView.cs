 using System;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

 public class TimerHeartsView : MonoBehaviour
 {
     [SerializeField] private Transform _container;
     [SerializeField] private Image _heartPrefab;

     private TimerService _timer;

     private readonly List<Image> _hearts = new List<Image>();
     private int _builtCount;

     public void Construct(TimerService timer)
     {
         _timer = timer;

         _timer.TimeChanged += OnTimeChanged;
         _timer.Finished += OnFinished;

         BuildIfNeeded(_timer.Duration);
         UpdateVisible(_timer.TimeLeft);
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
         BuildIfNeeded(duration);
         UpdateVisible(timeLeft);
     }

     private void OnFinished()
     {
         UpdateVisible(0f);
     }

     private void BuildIfNeeded(float duration)
     {
         int count = Mathf.CeilToInt(duration);

         if (count <= 0)
             count = 0;

         if (count == _builtCount)
             return;

         ClearHearts();

         for (int i = 0; i < count; i++)
         {
             Image heart = Instantiate(_heartPrefab, _container);
             _hearts.Add(heart);
         }

         _builtCount = count;
     }

     private void UpdateVisible(float timeLeft)
     {
         int secondsLeft = Mathf.CeilToInt(timeLeft);

         for (int i = 0; i < _hearts.Count; i++)
             _hearts[i].gameObject.SetActive(i < secondsLeft);
     }

     private void ClearHearts()
     {
         for (int i = 0; i < _hearts.Count; i++)
             Destroy(_hearts[i].gameObject);

         _hearts.Clear();
         _builtCount = 0;
     }
 }
