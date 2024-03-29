﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyColor : MonoBehaviour, IEndDragHandler
{
    public bool isWhite;
    public Image buttonColor;
    private PlayNote playNote;
    private GameManager gameManager;

    private void Start()
    {
        buttonColor = GetComponent<Image>();
        playNote = GetComponent<PlayNote>();
        gameManager = GameManager.instance;
        StartCoroutine(DisableNotes());
        StartCoroutine(EnableAllNotes());
    }

    private IEnumerator DisableNotes()
    {
        while (true)
        {
            while (NoteQuiz.gameActive == false || NoteQuiz.RoundIsFinished)
            {
                yield return null;
            }
            if (!playNote.noteData.isInKeys.Contains(gameManager.scale))
            {
                var alphaColor = buttonColor.color;
                alphaColor.a = 0.25f;
                alphaColor = Color.gray;
                buttonColor.color = alphaColor;
            }
            yield return null;
        }
    }

    private IEnumerator EnableAllNotes()
    {
        while (true)
        {
            while (PopupManager.changingScale == false)
            {
                yield return null;
            }
            RevertColor();
            yield return null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayNote.OnColorChange += ChangeColor;
        StartCoroutine(ResetColorChange());
    }

    private IEnumerator ResetColorChange()
    {
        while (!NoteQuiz.RoundIsFinished)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        PlayNote.OnColorChange -= ChangeColor;
        RevertColor();
    }

    public void ChangeColor()
    {
        if (NoteQuiz.answer.Contains(NoteQuiz.userInput) && !NoteQuiz.wrongOrder)
        {
            GreenColor();
        }
        else if (!NoteQuiz.answer.Contains(NoteQuiz.userInput) || NoteQuiz.wrongOrder)
        {
            RedColor();
        }
    }

    public void RevertColor()
    {
        if (isWhite == true)
        {
            buttonColor.color = new Color(250f/255f, 250f/255f, 250f/255f);
        }
        else
        {
            buttonColor.color = new Color(50f/255f, 50f/255f, 50f/255f);
        }
    }

    public void GreenColor()
    {
        buttonColor.color = new Color(0f/255f, 200f/255f, 0f/255f);
    }

    public void RedColor()
    {
        buttonColor.color = new Color(200f/255f, 0f/255f, 0f/255f);
    }
}
