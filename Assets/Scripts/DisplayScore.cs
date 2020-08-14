using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onScoreChanged += ShowScore;
        ShowScore();
    }

    private void ShowScore()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            currentScore.text = "Your current score is " + gameManager.currentScore.ToString();
        }
        else
        {
            currentScore.text = "Your current score is 0";
        }
        if (PlayerPrefs.HasKey("bestscore"))
        {
            if (gameManager.bestScore > 0 && gameManager.bestScore < gameManager.currentScore)
            {
                currentScore.text = string.Empty;
                bestScore.text = "Congratulations! " + gameManager.bestScore.ToString() + " is your best score";
            }
            else if (gameManager.bestScore > 0 && gameManager.bestScore > gameManager.currentScore)
            {
                bestScore.text = "Your best score is " + gameManager.bestScore.ToString();
            }
        }
        else
        {
            bestScore.text = string.Empty;
        }
    }
}
