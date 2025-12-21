using UnityEngine;

public class TimerRunner : MonoBehaviour
{
    public TimerService Timer { get; private set; }

    private void Awake()
    {
        Timer = new TimerService();
    }

    private void Update()
    {
        Timer.Tick(Time.deltaTime);
    }
}