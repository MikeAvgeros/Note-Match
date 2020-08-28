using Doozy.Engine.UI;
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
    public static bool gameHasStarted = false;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    public GameObject bestScoreImage;
    public GameObject lowScoreImage;
    public GameObject zeroScoreImage;

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
        PlayerPrefs.SetInt("bestscore", bestScore);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
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
    }

    private IEnumerator CloseGameOver()
    {
        UpdateBestScore();
        yield return new WaitForSeconds(0.5f);
        gameOverPopup.Hide();
        yield return new WaitForSeconds(0.5f);
        ResetScore();
        bestScoreImage.SetActive(false);
        lowScoreImage.SetActive(false);
        gameOverObject.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
