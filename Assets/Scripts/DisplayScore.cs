using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gamePoints;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onScoreChanged += UpdateScore;
        UpdateScore();
        gameManager.onPointsChanged += UpdatePoints;
        UpdatePoints();
    }

    private void UpdateScore()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            currentScore.text = gameManager.currentScore.ToString();
        }
        else
        {
            currentScore.text = "0";
        }
        if (PlayerPrefs.HasKey("bestscore"))
        {
            bestScore.text = gameManager.bestScore.ToString();
        }
        else
        {
            bestScore.text = "0";
        }
    }

    private void UpdatePoints()
    {
        if (PlayerPrefs.HasKey("points"))
        {
            gamePoints.text = gameManager.gamePoints.ToString();
        }
        else
        {
            gamePoints.text = "0";
        }
    }
}
