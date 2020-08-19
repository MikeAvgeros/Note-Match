using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public float startTime;
    public float currentTime;
    public bool timerGoing = false;
    private NoteQuiz noteQuiz;

    private void Start()
    {
        noteQuiz = GetComponent<NoteQuiz>();
    }

    private void Update()
    {
        if (timerGoing && noteQuiz.canStartTimer)
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
