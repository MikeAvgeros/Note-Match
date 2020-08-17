using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayTimer : MonoBehaviour
{
    private CountTimer countTimer;
    //public GameObject timerGameObject;
    //public GameObject roundedTimer;
    public TextMeshProUGUI timer;

    private void Start()
    {
        countTimer = GameObject.FindWithTag("Notequiz").GetComponent<CountTimer>();
        //timerGameObject.SetActive(false);
        //roundedTimer.SetActive(false);
        StartCoroutine(CheckTimer());
    }

    private IEnumerator CheckTimer()
    {
        while (true)
        {
            if (countTimer.timerHasStarted == true)
            {
                //timerGameObject.SetActive(true);
                //roundedTimer.SetActive(true);
                timer.text = countTimer.timeSpent.ToString();
            }
            else
            {
                //timerGameObject.SetActive(false);
                //roundedTimer.SetActive(false);
            }
            yield return null;
        }
    }
}
