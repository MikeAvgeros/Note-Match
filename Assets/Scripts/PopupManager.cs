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
    public UIPopup quizTextPopup;
    public TextMeshProUGUI quizText;
    public TextMeshProUGUI contactUsMessage;
    public AudioClip notification;
    public static bool changingScale = false;
    private AudioPoolManager audioPoolManager;

    private void Start()
    {
        if (GameManager.gameHasStarted == false)
        {
            OpenTutorialPopup();
        }
        audioPoolManager = AudioPoolManager.instance;
    }

    private IEnumerator ShowLevelSelectionPopup()
    {
        if (NoteQuiz.gameActive == true)
        {
            StartCoroutine(ShowQuizPopup());
            quizText.text = "Can't change level/scale" + "\n" + "while the game is active";
            yield return new WaitForSeconds(0.5f);
            audioPoolManager.PlayUISound(notification);
            yield return new WaitForSeconds(1.5f);
            quizTextPopup.Hide();
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            levelSelectionPopup.Show();
        }
    }

    private IEnumerator ShowQuizPopup()
    {
        if (quizTextPopup.IsVisible || quizTextPopup.IsHiding)
        {
            quizTextPopup.Hide();
            yield return new WaitForSeconds(0.5f);
            quizTextPopup.Show();
        }
        else
        {
            quizTextPopup.Show();
            yield return new WaitForSeconds(2f);
            quizTextPopup.Hide();
        }
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

    public void CloseButtonLevelSelection()
    {
        StartCoroutine(CloseLevelSelectionPopup());
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
        changingScale = true;
        StartCoroutine(ShowScaleSelectionPopup());
    }

    public void CloseScaleSelection()
    {
        changingScale = false;
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
