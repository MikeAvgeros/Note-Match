using System.Collections;
using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public int startTime;
    public int timeSpent;
    public bool timerHasStarted = false;
    //public Transform roundedTimer;

    public void StartTimer()
    {
        timerHasStarted = true;
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopCoroutine(Timer());
        timerHasStarted = false;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            if (timerHasStarted)
            yield return new WaitForSeconds(1f);
            timeSpent++;
            //roundedTimer.GetComponent<Image>().fillAmount -= 1f / originalTime;
            yield return null;
        }
    }
}
