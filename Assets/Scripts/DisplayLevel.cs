﻿using System.Collections;
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
            if (PlayerPrefs.HasKey("level"))
            {
                level.text = gameManager.level.ToString();
            }
            yield return null;
        }
    }
}
