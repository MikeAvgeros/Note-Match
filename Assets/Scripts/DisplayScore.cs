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
    }

    private void Update()
    {
        ShowScore();
    }

    private void ShowScore()
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
}
