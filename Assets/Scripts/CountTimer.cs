using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour
{
    public int originalTime;
    public int timeLeft;
    public bool timerHasStarted = false;
    public Transform roundedTimer;

    public void StartTimer()
    {
        StartCoroutine(Timer());
        timerHasStarted = true;
    }

    public void StopTimer()
    {
        StopCoroutine(Timer());
        timerHasStarted = false;
    }

    private void Update()
    {
        if (timeLeft < 0)
        {
            timeLeft = 0;
            RestartTimer();
        }
    }

    public void RestartTimer()
    {
        timeLeft = originalTime;
        roundedTimer.GetComponent<Image>().fillAmount = 1f;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            roundedTimer.GetComponent<Image>().fillAmount -= 1f / originalTime;
            yield return null;
        }
    }
}
