using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    public TextMeshProUGUI level;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level.text = gameManager.level.ToString();
        }
    }
}
