using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayTimer : MonoBehaviour
{
    private CountTimer countTimer;
    public TextMeshProUGUI timer;

    private void Start()
    {
        countTimer = GameObject.FindWithTag("Notequiz").GetComponent<CountTimer>();
        timer.text = "0";
        StartCoroutine(CheckTimer());
    }

    private IEnumerator CheckTimer()
    {
        while (true)
        {
            if (countTimer.timerGoing)
            {
                timer.text = countTimer.currentTime.ToString("0");
            }
            yield return null;
        }
    }
}
