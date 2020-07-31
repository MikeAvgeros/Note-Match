using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityAction onScoreChanged;
    public UnityAction onPointsChanged;
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public int currentScore;
    public int bestScore;
    public int gamePoints;
    public int level;
    public string scale;

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
        PlayerPrefs.SetInt("points", gamePoints);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetString("scale", scale);
    }

    public void UpdateScore(int s)
    {
        currentScore += s;
        PlayerPrefs.SetInt("score", currentScore);
        onScoreChanged?.Invoke();
    }

    public void UpdatePoints(int p)
    {
        gamePoints += p;
        PlayerPrefs.SetInt("points", gamePoints);
        onPointsChanged?.Invoke();
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

    public void QuitApplication()
    {
        Application.Quit();
    }
}
