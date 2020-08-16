using System.Collections;
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
        StartCoroutine(ShowScore());
    }

    private IEnumerator ShowScore()
    {
        while (true)
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
            yield return null;
        }
    }
}
