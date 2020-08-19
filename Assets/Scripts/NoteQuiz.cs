using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using Doozy.Engine.UI;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI resultText;
    public static CountTimer countTimer;
    public UIPopup quizTextPopup;
    public UIPopup resultTextPopup;
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
    }

    public void PlayGame()
    {
        ResetGameValues();
        if (currentRoundNotes != null)
        {
            currentRoundNotes.Clear();
        }
        if (playableNotes == null || playableNotes.Count < gameManager.level)
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
        resultTextPopup.Hide();
        quizTextPopup.Show();
        if (gameManager.level == 1)
        {
            quizText.text = "Find the note" + "\n" + "and swipe up";
        }
        else
        {
            quizText.text = "Find the notes" + "\n" + "and swipe up";
        }
        StartCoroutine(PlayRandomNote());
        countTimer.StartTimer();
    }

    private void ResetGameValues()
    {
        currentRoundNotesID = string.Empty;
        currentRoundNotesName = string.Empty;
        answer = string.Empty;
        userInput = string.Empty;
        wrongOrder = false;
        countTimer.RestartTimer();
    }

    private void GameActive()
    {
        gameActive = true;
        notesToPlay = 0;
    }

    private IEnumerator PlayRandomNote()
    {
        yield return new WaitForSeconds(0.5f);
        if (playableNotes.Count > 0 && currentRoundAnswerIDList.Count == 0)
        {
            GameActive();
            GetNotes();
            StartCoroutine(PlayNotes());
            GetAnswer();
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
        StartCoroutine(PlayNotes());
    }

    public void RegisterAnswer()
    {
        if (userInput == answer)
        {
            WellDoneText();
            gameManager.UpdateScore(gameManager.level * gameManager.level);
        }
        else
        {
            ShowCorrectAnswer();
            StartCoroutine(GameOver());
        }
        ResetRound();
    }

    private void WellDoneText()
    {
        resultTextPopup.Show();
        resultText.text = "Well Done!" + "\n" + "Play again";
    }

    private void ResetRound()
    {
        countTimer.StopTimer();
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

    private void ShowCorrectAnswer()
    {
        foreach (NoteData currentRoundNote in currentRoundNotes)
        {
            currentRoundNotesName += currentRoundNote.name + " ";
        }
        resultTextPopup.Show();
        if (gameManager.level == 1)
        {
            resultText.text = "The correct note is " + currentRoundNote.name;
        }
        else
        {
            resultText.text = "The correct notes are " + currentRoundNotesName;
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameManager.UpdateBestScore();
        gameManager.OpenGameOverPopup();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
