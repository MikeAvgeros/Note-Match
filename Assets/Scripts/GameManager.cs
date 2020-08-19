﻿using Doozy.Engine.UI;
using System.Collections;
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

    private void Awake()
    {
        Application.targetFrameRate = 30;
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
        PlayerPrefs.SetInt("bestscore", bestScore);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetString("scale", scale);
        gameOverObject.SetActive(false);
    }

    private void Start()
    {
        CloseGameOverPopup();
        gameOverObject.SetActive(false);
    }

    public void CheckForBestScore()
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

    public void UpdateBestScore()
    {
        PlayerPrefs.SetInt("bestscore", bestScore);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
    }

    public void ResetScore()
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

    public void OpenGameOverPopup()
    {
        gameOverObject.SetActive(true);
        gameOverPopup.Show();
    }

    public void CloseGameOverPopup()
    {
        StartCoroutine(CloseGameOver());
    }

    private IEnumerator CloseGameOver()
    {
        UpdateBestScore();
        yield return new WaitForSeconds(0.5f);
        gameOverPopup.Hide();
        yield return new WaitForSeconds(0.5f);
        ResetScore();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
