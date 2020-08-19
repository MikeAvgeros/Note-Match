using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public float startTime;
    public float currentTime;
    public bool timerGoing = false;

    private void Update()
    {
        if (timerGoing)
        {
            Timer();
        }
    }

    public void StartTimer()
    {
        timerGoing = true;
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    public void RestartTimer()
    {
        currentTime = startTime;
    }

    private void Timer()
    {
        currentTime += 1 * Time.deltaTime;
    }
}
