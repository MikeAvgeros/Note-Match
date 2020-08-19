using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayScale : MonoBehaviour
{
    public TextMeshProUGUI scale;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        StartCoroutine(ShowScale());
    }

    private IEnumerator ShowScale()
    {
        while (true)
        {
            if (PlayerPrefs.HasKey("scale"))
            {
                scale.text = gameManager.scale.ToString();
            }
            yield return null;
        }
    }
}
