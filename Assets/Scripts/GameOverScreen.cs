using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        ShowGameOverScreen();
    }

    private void ShowGameOverScreen()
    {
        if (gameManager.currentScore > gameManager.bestScore)
        {
            titleText.text = "New Best Score!";
        }
        else
        {
            titleText.text = "Good Game!";
        }
        scoreText.text = gameManager.currentScore.ToString();
    }
}
