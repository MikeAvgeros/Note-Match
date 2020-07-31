using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;

public class NoteQuiz : MonoBehaviour
{
    public NoteData[] notesList;
    public List<NoteData> playableNotes;
    public static NoteData currentRoundNote;
    public List<NoteData> currentRoundNotes;
    public static List<string> currentRoundAnswerIDList = new List<string>();
    private GameManager gameManager;
    private AudioPoolManager audioPoolManager;
    public Transform audioPoolManagerTransform;
    public TextMeshProUGUI quizText;
    public static CountTimer countTimer;
    public UIPopup quizTextPopup;
    public static string userInput;
    public static bool gameActive;
    public static string currentRoundNotesID;
    public static string currentRoundNotesName;
    private int notesToPlay;
    private int playedNotes;
    private int currentIndex = -1;
    public static bool wrongOrder;
    public Func<string, int> AnswerLength;
    public Func<string, string> Answer;
    public static string answer;
    public static int answerLength;
    public bool useTimer;
    public static int notesLeft;

    public static bool RoundIsFinished => userInput.Length >= answerLength || !answer.Contains(userInput) || wrongOrder == true;

    private void Start()
    {
        gameManager = GameManager.instance;
        audioPoolManager = AudioPoolManager.instance;
        countTimer = GetComponent<CountTimer>();
        gameActive = false;
        answer = string.Empty;
        userInput = string.Empty;
        wrongOrder = false;
        countTimer.timeLeft = 0;
        useTimer = false;
        if (quizTextPopup == null)
        {
            quizTextPopup = GameObject.FindWithTag("Quiztextpopup").GetComponent<UIPopup>();
        }
        if (quizText == null)
        {
            quizText = GameObject.FindWithTag("Quiztext").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UseTimer()
    {
        useTimer = true;
    }

    public void PlayGame()
    {
        ResetGameValues();
        if (currentRoundNotes != null)
        {
            currentRoundNotes.Clear();
        }
        if (playableNotes == null || playableNotes.Count == 0)
        {
            foreach (NoteData noteData in notesList)
            {
                if (noteData.isInKeys.Contains(gameManager.scale))
                {
                    playableNotes.Add(noteData);
                }
            }
        }
        notesLeft = gameManager.level;
        quizTextPopup.Show();
        if (gameManager.level == 1)
        {
            quizText.text = "Find the note";
        }
        else
        {
            quizText.text = "Find the notes";
        }
        StartCoroutine(PlayRandomNote());
        if (useTimer == true)
        {
            StartCoroutine(CheckTimer());
            if (countTimer.timeLeft > 0)
            {
                return;
            }
            countTimer.RestartTimer();
            countTimer.StartTimer();
        }
    }

    private void ResetGameValues()
    {
        currentRoundNotesID = string.Empty;
        currentRoundNotesName = string.Empty;
        answer = string.Empty;
        userInput = string.Empty;
        wrongOrder = false;
    }

    private void GameActive()
    {
        gameActive = true;
        notesToPlay = 0;
    }

    private IEnumerator CheckTimer()
    {
      while (gameActive == false || gameActive == true && countTimer.timeLeft > 0)
      {
         yield return null;
      }
      StartCoroutine(TimesUp());
    }

    private IEnumerator TimesUp()
    {
        foreach (AudioSource audioSource in audioPoolManager.audioSourcePool)
        {
            audioPoolManager.StopNoteSound(audioSource);
        }
        countTimer.timerHasStarted = false;
        quizTextPopup.Show();
        quizText.text = "Time's up." + " Game Has Finished";
        yield return new WaitForSeconds(1f);
        ResetRound();
        StartCoroutine(GameOver());
    }

    private IEnumerator PlayRandomNote()
    {
        var playDelay = new WaitForSeconds(0.5f);
        yield return playDelay;
        if (playableNotes.Count > 0 && currentRoundAnswerIDList.Count == 0)
        {
            GameActive();
            GetNotes();
            StartCoroutine(PlayNotes());
            GetAnswer();
            yield return playDelay;
            quizTextPopup.Hide();
            quizText.text = string.Empty;
        }
    }

    private void GetNotes()
    {
        while (notesToPlay < gameManager.level)
        {
            int randomIndex = UnityEngine.Random.Range(0, playableNotes.Count);
            currentRoundNote = playableNotes[randomIndex];
            currentRoundNotes.Add(currentRoundNote);
            playableNotes.Remove(currentRoundNote);
            notesToPlay++;
        }
        foreach (NoteData currentRoundNote in currentRoundNotes)
        {
            Debug.Log(currentRoundNote);
        }
    }

    private IEnumerator PlayNotes()
    {
        playedNotes = 0;
        do
        {
            PlayNoteSequence();
            playedNotes++;
            yield return new WaitForSeconds(1f);
        }
        while (playedNotes < currentRoundNotes.Count);
    }

    private void PlayNoteSequence()
    {
        if (currentRoundNotes.Count > 0)
        {
            currentIndex = GetIndex();
            NoteData currentNote = currentRoundNotes[currentIndex];
            audioPoolManager.PlayNoteSound(currentNote);
        }
    }

    private int GetIndex()
    {
        var index = currentIndex;
        if (currentRoundNotes.Count > 1)
        {
            index++;
            if (index > currentRoundNotes.Count - 1)
            {
                index = 0;
            }
        }
        else
        {
            index = 0;
        }
        return index;
    }

    private void GetAnswer()
    {
        foreach (NoteData currentRoundNote in currentRoundNotes)
        {
            currentRoundNotesID += currentRoundNote.ID.ToString();
            currentRoundAnswerIDList.Add(currentRoundNote.ID.ToString());
        }
        AnswerLength = GetAnswerLength;
        answerLength = AnswerLength(currentRoundNotesID);
        Answer = GetAnswerName;
        answer = Answer(currentRoundNotesID);
    }

    private int GetAnswerLength(string answer)
    {
        return answer.Length;
    }

    private string GetAnswerName(string name)
    {
        return name;
    }

    public void ReplayNotes()
    {
        gameManager.UpdatePoints(-10);
        StartCoroutine(PlayNotes());
    }

    public void RegisterAnswer()
    {
        if (userInput == answer)
        {
            StartCoroutine(WellDoneText());
            gameManager.UpdateScore(gameManager.level * 1);
            gameManager.UpdatePoints(gameManager.level * 10);
        }
        else
        {
            gameManager.UpdatePoints(-10);
            StartCoroutine(ShowCorrectAnswer());
            StartCoroutine(GameOver());
        }
        ResetRound();
    }

    private IEnumerator WellDoneText()
    {
        quizTextPopup.Show();
        quizText.text = "Well Done!";
        yield return new WaitForSeconds(2f);
        quizTextPopup.Hide();
        quizText.text = string.Empty;
    }

    private void ResetRound()
    {
        audioPoolManager.audioSourcePool.Clear();
        currentRoundAnswerIDList.Clear();
        NoteLabel.userInputList.Clear();
        if (audioPoolManagerTransform == null)
        {
            audioPoolManagerTransform = GameObject.FindWithTag("Audiomanager").GetComponent<Transform>();
        }
        foreach (Transform child in audioPoolManagerTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator ShowCorrectAnswer()
    {
        foreach (NoteData currentRoundNote in currentRoundNotes)
        {
            currentRoundNotesName += currentRoundNote.name + " ";
        }
        quizTextPopup.Show();
        if (gameManager.level == 1)
        {
            quizText.text = "The correct note is " + currentRoundNote.name;
        }
        else
        {
            quizText.text = "The correct notes are " + currentRoundNotesName;
        }
        yield return new WaitForSeconds(2f);
        quizTextPopup.Hide();
        quizText.text = string.Empty;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        gameManager.UpdateBestScore();
        gameManager.ResetScore();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
