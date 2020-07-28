﻿using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayNote : MonoBehaviour, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public NoteData noteData;
    public NoteQuiz noteQuiz;
    public TextMeshProUGUI quizText;
    private AudioPoolManager audioPoolManager;
    private GameManager gameManager;
    public delegate void UpdateColor();
    public static event UpdateColor OnColorChange;
    public delegate void UserInput();
    public static event UserInput OnUserInput;
    private float difference;

    private void Start()
    {
        audioPoolManager = AudioPoolManager.instance;
        gameManager = GameManager.instance;
        noteQuiz = GameObject.FindWithTag("Notequiz").GetComponent<NoteQuiz>();
        quizText = GameObject.FindWithTag("Quiztext").GetComponent<TextMeshProUGUI>();
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
        if (quizText == null)
        {
            quizText = GameObject.FindWithTag("Quiztext").GetComponent<TextMeshProUGUI>();
        }
        quizText.text = noteData.noteName + " is not in " + gameManager.scale;
        yield return new WaitForSeconds(1f);
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
