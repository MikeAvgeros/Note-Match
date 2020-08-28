﻿using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using Doozy.Engine.UI;

public class PlayNote : MonoBehaviour, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public NoteData noteData;
    public NoteQuiz noteQuiz;
    public TextMeshProUGUI quizText;
    private AudioPoolManager audioPoolManager;
    private GameManager gameManager;
    public UIPopup quizTextPopup;
    public delegate void UpdateColor();
    public static event UpdateColor OnColorChange;
    public delegate void UserInput();
    public static event UserInput OnUserInput;
    private float difference;
    public AudioClip correctSwipe;
    public AudioClip notification;

    private void Start()
    {
        audioPoolManager = AudioPoolManager.instance;
        gameManager = GameManager.instance;
    }

    private void Play()
    {
        audioPoolManager.PlayNoteSound(noteData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (NoteQuiz.gameActive == false || NoteQuiz.gameActive == true && noteData.isInKeys.Contains(gameManager.scale))
        {
            Play();
        }
        else
        {
            StartCoroutine(QuizText());
        }
    }

    private IEnumerator QuizText()
    {
        quizTextPopup.Show();
        quizText.text = noteData.noteName + " is not in " + gameManager.scale;
        yield return new WaitForSeconds(0.5f);
        audioPoolManager.PlayUISound(notification);
        yield return new WaitForSeconds(1f);
        quizTextPopup.Hide();
        quizText.text = string.Empty;
    }

    public void OnDrag(PointerEventData eventData)
    {
        difference = eventData.pressPosition.y - eventData.position.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (difference < 0)
        {
            if (NoteQuiz.gameActive == true)
            {
                foreach (AudioSource audioSource in audioPoolManager.audioSourcePool)
                {
                    audioPoolManager.StopNoteSound(audioSource);
                }
                if (NoteQuiz.RoundIsFinished)
                {
                    return;
                }
                OnUserInput?.Invoke();
                CheckOrderofAnswer();
                OnColorChange?.Invoke();
                if (NoteQuiz.answer.Contains(NoteQuiz.userInput) && !NoteQuiz.wrongOrder)
                {
                    if (!NoteQuiz.RoundIsFinished)
                    {
                        audioPoolManager.PlayUISound(correctSwipe);
                    }
                    NoteQuiz.notesLeft--;
                }
                if (NoteQuiz.RoundIsFinished)
                {
                    noteQuiz.RegisterAnswer();
                }
            }
        }
    }

    private void CheckOrderofAnswer()
    {
        if (NoteQuiz.currentRoundAnswerIDList != null && NoteLabel.userInputList !=null && NoteQuiz.currentRoundAnswerIDList.Count > 1)
        {
            if (NoteLabel.userInputList[NoteLabel.userInputList.Count - 1] == NoteQuiz.currentRoundAnswerIDList[NoteLabel.userInputList.Count - 1])
            {
                NoteQuiz.wrongOrder = false;
            }
            else
            {
                NoteQuiz.wrongOrder = true;
            }
        }
    }
}
