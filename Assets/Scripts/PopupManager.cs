using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public UIPopup settingsPopup;
    public UIPopup levelSelectionPopup;
    public UIPopup scaleSelectionPopup;

    private void Start()
    {
        StartCoroutine(ShowLevelSelectionPopup());
    }

    private IEnumerator ShowLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.5f);
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

    public void OpenSettingsPopup()
    {
        StartCoroutine(OpenSettings());
    }

    private IEnumerator OpenSettings()
    {
        yield return new WaitForSeconds(0.5f);
        settingsPopup.Show();
    }

    public void CloseSettingsPopup()
    {
        StartCoroutine(CloseSettings());
    }

    private IEnumerator CloseSettings()
    {
        yield return new WaitForSeconds(0.5f);
        settingsPopup.Hide();
    }
}
