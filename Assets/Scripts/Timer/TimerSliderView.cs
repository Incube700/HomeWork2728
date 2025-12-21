using System;
using UnityEngine.UI;
using UnityEngine;

public class TimerSliderView : MonoBehaviour
{
   [SerializeField] private TimerRunner _runner;
   [SerializeField] private Slider _slider;

   private TimerService _timer;

   private void Start()
   {
      _timer = _runner.Timer;

      _timer.NormalizedChanged += ONNormalizedChanged;

      ONNormalizedChanged(1f);
   }

   private void OnDestroy()
   {
      if (_timer != null)
         _timer.NormalizedChanged -= ONNormalizedChanged;
   }
   
   private void ONNormalizedChanged(float normalized)
   {
     _slider.value = normalized;
   }
}
