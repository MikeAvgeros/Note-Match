using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public UIPopup settingsPopup;
    public UIPopup tutorialPopup;
    public UIPopup levelSelectionPopup;
    public UIPopup scaleSelectionPopup;

    private void Start()
    {
        StartCoroutine(ShowLevelSelectionPopup());
    }

    private IEnumerator ShowLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.4f);
        levelSelectionPopup.Show();
    }

    private IEnumerator CloseLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(0.5f);
        scaleSelectionPopup.Show();
    }

    private IEnumerator CloseScaleSelectionPopup()
    {
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(0.4f);
        settingsPopup.Show();
    }

    public void CloseSettingsPopup()
    {
        StartCoroutine(CloseSettings());
    }

    private IEnumerator CloseSettings()
    {
        yield return new WaitForSeconds(0.4f);
        settingsPopup.Hide();
    }

    public void OpenTutorialPopup()
    {
        StartCoroutine(OpenTutorial());
    }

    private IEnumerator OpenTutorial()
    {
        yield return new WaitForSeconds(0.4f);
        tutorialPopup.Show();
    }

    public void CloseTutorialPopup()
    {
        StartCoroutine(CloseTutorial());
    }

    private IEnumerator CloseTutorial()
    {
        yield return new WaitForSeconds(0.4f);
        tutorialPopup.Hide();
    }
}
