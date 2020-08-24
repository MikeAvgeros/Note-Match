using Doozy.Engine.UI;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public UIPopup contactUsPopup;
    public UIPopup tutorialPopup;
    public UIPopup levelSelectionPopup;
    public UIPopup scaleSelectionPopup;
    public TextMeshProUGUI contactUsMessage;

    private IEnumerator ShowLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.2f);
        levelSelectionPopup.Show();
    }

    private IEnumerator CloseLevelSelectionPopup()
    {
        yield return new WaitForSeconds(0.2f);
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
        yield return new WaitForSeconds(0.3f);
        scaleSelectionPopup.Show();
    }

    private IEnumerator CloseScaleSelectionPopup()
    {
        yield return new WaitForSeconds(0.2f);
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

    public void OpenContactUsPopup()
    {
        StartCoroutine(OpenContactUs());
    }

    private IEnumerator OpenContactUs()
    {
        contactUsMessage.text = "We want to hear your feedback";
        yield return new WaitForSeconds(0.2f);
        contactUsPopup.Show();
    }

    public void CloseContactUsPopup()
    {
        StartCoroutine(CloseContactUs());
    }

    private IEnumerator CloseContactUs()
    {
        yield return new WaitForSeconds(0.2f);
        contactUsPopup.Hide();
    }

    public void OpenTutorialPopup()
    {
        StartCoroutine(OpenTutorial());
    }

    private IEnumerator OpenTutorial()
    {
        yield return new WaitForSeconds(0.2f);
        tutorialPopup.Show();
    }

    public void CloseTutorialPopup()
    {
        StartCoroutine(CloseTutorial());
    }

    private IEnumerator CloseTutorial()
    {
        yield return new WaitForSeconds(0.2f);
        tutorialPopup.Hide();
    }
}
