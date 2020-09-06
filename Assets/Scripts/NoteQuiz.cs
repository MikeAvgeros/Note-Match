using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using Doozy.Engine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteQuiz : MonoBehaviour
{
    public NoteData[] notesList;
    public static List<NoteData> playableNotes = new List<NoteData>();
    public static NoteData currentRoundNote;
    public static List<NoteData> currentRoundNotes = new List<NoteData>();
    public static List<string> currentRoundAnswerIDList = new List<string>();
    private GameManager gameManager;
    private AudioPoolManager audioPoolManager;
    public TextMeshProUGUI quizText;
    public TextMeshProUGUI resultText;
    public static CountTimer countTimer;
    public UIPopup quizTextPopup;
    public UIPopup resultTextPopup;
    public UIButton levelSelectButton;
    public UIButton playButton;
    public UIButton replayButton;
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioClip notification;
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
    public bool canStartTimer;

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
        resultTextPopup.Hide();
        StartCoroutine(ShowQuizPopup());
        if (gameManager.level == 0 || gameManager.scale == string.Empty)
        {
            quizText.text = "You need to choose" + "\n" + "a level and a scale";
            StartCoroutine(ButtonFinder(levelSelectButton));
            return;
        }
        if (gameActive == false)
        {
            ResetGameValues();
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
            if (gameManager.level == 1)
            {
                quizText.text = "Find the correct note" + "\n" + "and swipe up in order";
            }
            else
            {
                quizText.text = "Find the correct notes" + "\n" + "and swipe up in order";
            }
            StartCoroutine(PlayRandomNote());
            countTimer.StartTimer();
        }
        else if (gameActive == true)
        {
            StartCoroutine(ButtonFinder(replayButton));
            quizText.text = "The game has started" + "\n" + "Press replay to repeat";
        }
    }

    private IEnumerator ShowQuizPopup()
    {
        if (quizTextPopup.IsVisible)
        {
            quizTextPopup.Hide();
            yield return new WaitForSeconds(1f);
            quizTextPopup.Show();
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
        }
        else if (quizTextPopup.IsShowing)
        {
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
            yield return new WaitForSeconds(0.5f);
            quizTextPopup.Show();
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
        }
        else if (quizTextPopup.IsHidden)
        {
            quizTextPopup.Show();
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
        }
        else if (quizTextPopup.IsHiding)
        {
            yield return new WaitForSeconds(1f);
            quizTextPopup.Show();
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
        }
    }

    private IEnumerator ButtonFinder(UIButton button)
    {
        yield return new WaitForSeconds(0.5f);
        audioPoolManager.PlayUISound(notification);
        yield return new WaitForSeconds(1f);
        button.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1f);
        button.GetComponent<Image>().color = Color.white;
    }

    private void ResetGameValues()
    {
        currentRoundNotesID = string.Empty;
        currentRoundNotesName = string.Empty;
        answer = string.Empty;
        answerLength = 0;
        userInput = string.Empty;
        wrongOrder = false;
        countTimer.RestartTimer();
        canStartTimer = false;
        notesLeft = gameManager.level;
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
            StartCoroutine(CanStartTimer());
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
        if (currentRoundNotes.Count > 0 && gameActive == true)
        {
            currentIndex = GetIndex();
            NoteData currentNote = currentRoundNotes[currentIndex];
            audioPoolManager.PlayNoteSound(currentNote);
        }
    }

    private int GetIndex()
    {
        var index = currentIndex;
        if (currentRoundNotes.Count > 1 && playedNotes > 0)
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

    private IEnumerator CanStartTimer()
    {
        yield return new WaitForSeconds(gameManager.level);
        canStartTimer = true;
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
        if (gameActive == true)
        {
            StartCoroutine(WaitAndReplay());
        }
        else
        {
            StartCoroutine(ButtonFinder(playButton));
            StartCoroutine(ShowQuizPopup());
            quizText.text = "Press the play button" + "\n" + "to start the game";
        }
    }

    private IEnumerator WaitAndReplay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayNotes());
    }

    public void RegisterAnswer()
    {
        if (userInput == answer)
        {
            audioPoolManager.PlayUISound(correctAnswer);
            WellDoneText();
            gameManager.UpdateScore(gameManager.level * gameManager.level);
            ClearNoteLists();
        }
        else
        {
            audioPoolManager.PlayUISound(wrongAnswer);
            GameOverText();
            StartCoroutine(GameOver());
        }
        ResetRound();
    }

    private void WellDoneText()
    {
        resultTextPopup.Show();
        resultText.text = "Congratulations" + "\n" + "Play again";
    }

    private void GameOverText()
    {
        resultTextPopup.Show();
        resultText.text = "Wrong answer :(" +"\n" + "It's Game Over";
    }

    private void ResetRound()
    {
        gameActive = false;
        countTimer.StopTimer();
        currentRoundAnswerIDList.Clear();
        NoteLabel.userInputList.Clear();
    }

    public static void ClearNoteLists()
    {
        if (currentRoundNotes != null)
        {
            currentRoundNotes.Clear();
        }
        if (playableNotes != null)
        {
            playableNotes.Clear();
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameManager.OpenGameOverPopup();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
