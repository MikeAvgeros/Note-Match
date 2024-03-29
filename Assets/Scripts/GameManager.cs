﻿using Doozy.Engine.UI;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public UIPopup gameOverPopup;
    public GameObject gameOverObject;
    public int currentScore;
    public int bestScore;
    public int level;
    public string scale;
    public bool newBestScore;
    public bool canPlayGOAudio = false;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI answertext;
    public GameObject bestScoreImage;
    public GameObject lowScoreImage;
    public GameObject zeroScoreImage;

    private void Awake()
    {
        Application.targetFrameRate = -1;
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        PlayerPrefs.SetInt("score", currentScore);
        bestScore = PlayerPrefs.GetInt("bestscore");
        PlayerPrefs.SetInt("bestscore", bestScore);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetString("scale", scale);
        gameOverObject.SetActive(false);
    }

    private void CheckForBestScore()
    {
        if (currentScore > bestScore)
        {
            newBestScore = true;
        }
        else
        {
            newBestScore = false;
        }
    }

    public void UpdateScore(int s)
    {
        currentScore += s;
        PlayerPrefs.SetInt("score", currentScore);
    }

    private void UpdateBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
        PlayerPrefs.SetInt("bestscore", bestScore);
    }

    private void ResetScore()
    {
        currentScore = 0;
    }

    public void UpdateLevel(int l)
    {
        level = l;
        PlayerPrefs.SetInt("level", level);
    }

    public void UpdateScale(string s)
    {
        scale = s;
        PlayerPrefs.SetString("scale", scale);
    }

    private void ShowGameOverScreen()
    {
        CheckForBestScore();
        if (newBestScore == true)
        {
            titleText.text = "New Best Score!";
            bestScoreImage.SetActive(true);
            lowScoreImage.SetActive(false);
            zeroScoreImage.SetActive(false);
        }
        else if (newBestScore == false && currentScore > 0)
        {
            titleText.text = "Good Game!";
            bestScoreImage.SetActive(false);
            lowScoreImage.SetActive(true);
            zeroScoreImage.SetActive(false);
        }
        else
        {
            titleText.text = "Try again!";
            bestScoreImage.SetActive(false);
            lowScoreImage.SetActive(false);
            zeroScoreImage.SetActive(true);
        }
        scoreText.text = currentScore.ToString();
        ShowCorrectAnswer();
    }

    public void OpenGameOverPopup()
    {
        canPlayGOAudio = true;
        gameOverObject.SetActive(true);
        ShowGameOverScreen();
        gameOverPopup.Show();
    }

    public void CloseGameOverPopup()
    {
        StartCoroutine(CloseGameOver());
        NoteQuiz.ClearNoteLists();
    }

    private IEnumerator CloseGameOver()
    {
        UpdateBestScore();
        yield return new WaitForSeconds(0.5f);
        ResetScore();
        gameOverPopup.Hide();
        yield return new WaitForSeconds(0.5f);
        bestScoreImage.SetActive(false);
        lowScoreImage.SetActive(false);
        gameOverObject.SetActive(false);
    }

    private void ShowCorrectAnswer()
    {
        foreach (NoteData currentRoundNote in NoteQuiz.currentRoundNotes)
        {
            NoteQuiz.currentRoundNotesName += " " + currentRoundNote.octaveName + " ";
        }
        if (level == 1)
        {
            answertext.text = "The correct note was " + NoteQuiz.currentRoundNote.octaveName;
        }
        else
        {
            answertext.text = "The correct notes were" + NoteQuiz.currentRoundNotesName;
        }
    }

    public void QuitApplication()
    {
        StartCoroutine(CloseGame());
    }

    private IEnumerator CloseGame()
    {
        UpdateBestScore();
        yield return new WaitForSeconds(0.4f);
        ResetScore();
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
    }
}
