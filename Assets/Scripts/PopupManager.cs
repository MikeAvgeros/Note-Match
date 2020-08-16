using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public UIPopup ratePopup;
    public UIPopup shopPopup;
    public UIPopup settingsPopup;
    public UIPopup levelSelectionPopup;
    public UIPopup scaleSelectionPopup;

    private void Start()
    {
        StartCoroutine(ShowLevelSelectionPopup());
    }

    private IEnumerator ShowLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.1f);
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
        shopPopup.Show();
    }

    public void CloseShopPopup()
    {
        shopPopup.Hide();
    }

    public void OpenRatePopup()
    {
        ratePopup.Show();
    }

    public void CloseRatePopup()
    {
        ratePopup.Hide();
    }

    public void OpenSettingsPopup()
    {
        settingsPopup.Show();
    }

    public void CloseSettingsPopup()
    {
        settingsPopup.Hide();
    }

    public void CloseScorePopup()
    {
        settingsPopup.Hide();
    }
}
