using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    public TextMeshProUGUI level;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        StartCoroutine(ShowLevel());
    }

    private IEnumerator ShowLevel()
    {
        while (true)
        {
            if (PlayerPrefs.HasKey("level") && gameManager.level != 0)
            {
                level.text = gameManager.level.ToString();
            }
            else
            {
                level.text = string.Empty;
            }
            yield return null;
        }
    }
}
