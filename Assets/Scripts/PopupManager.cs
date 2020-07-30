using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public UIPopup ratePopup;
    public UIPopup shopPopup;
    public UIPopup settingsPopup;
    public UIPopup scorePopup;
    public UIPopup levelSelectionPopup;
    public UIPopup scaleSelectionPopup;

    private void Start()
    {
        StartCoroutine(ShowLevelSelectionPopup());
    }

    private IEnumerator ShowLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.1f);
        if (levelSelectionPopup == null)
        {
            levelSelectionPopup = GameObject.FindWithTag("Level").GetComponent<UIPopup>();
        }
        levelSelectionPopup.Show();
    }

    private IEnumerator CloseLevelSelectionPopup()
    {
        yield return new WaitForSeconds(1f);
        levelSelectionPopup.Hide();
    }

    public void OpenLevelSelection()
    {
        StartCoroutine(ShowLevelSelectionPopup());
    }

    public void CloseLevelSelection()
    {
        StartCoroutine(CloseLevelSelectionPopup());
        OpenScaleSelection();
    }

    private IEnumerator ShowScaleSelectionPopup()
    {
        yield return new WaitForSeconds(1f);
        if (scaleSelectionPopup == null)
        {
            scaleSelectionPopup = GameObject.FindWithTag("Scale").GetComponent<UIPopup>();
        }
        scaleSelectionPopup.Show();
    }

    private IEnumerator CloseScaleSelectionPopup()
    {
        yield return new WaitForSeconds(1f);
        scaleSelectionPopup.Hide();
    }

    public void OpenScaleSelection()
    {
        StartCoroutine(ShowScaleSelectionPopup());
    }

    public void CloseScaleSelection()
    {
        StartCoroutine(CloseScaleSelectionPopup());
    }

    public void OpenShopPopup()
    {
        if (shopPopup == null)
        {
            shopPopup = GameObject.FindWithTag("Shop").GetComponent<UIPopup>();
        }
        shopPopup.Show();
    }

    public void CloseShopPopup()
    {
        shopPopup.Hide();
    }

    public void OpenRatePopup()
    {
        if (ratePopup == null)
        {
            ratePopup = GameObject.FindWithTag("Rate").GetComponent<UIPopup>();
        }
        ratePopup.Show();
    }

    public void CloseRatePopup()
    {
        ratePopup.Hide();
    }

    public void OpenSettingsPopup()
    {
        if (settingsPopup == null)
        {
            settingsPopup = GameObject.FindWithTag("Settings").GetComponent<UIPopup>();
        }
        settingsPopup.Show();
    }

    public void CloseSettingsPopup()
    {
        settingsPopup.Hide();
    }

    public void OpenScorePopup()
    {
        if (scorePopup == null)
        {
            scorePopup = GameObject.FindWithTag("Score").GetComponent<UIPopup>();
        }
        scorePopup.Show();
    }

    public void CloseScorePopup()
    {
        settingsPopup.Hide();
    }
}
