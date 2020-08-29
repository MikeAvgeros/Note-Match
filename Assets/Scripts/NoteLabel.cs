using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteLabel : MonoBehaviour, IPointerUpHandler
{
    public string NoteID => noteID;
    [SerializeField] private string noteID;
    public static List<string> userInputList;

    private void Awake()
    {
        userInputList = new List<string>();
    }

    public void UpdateUserInput()
    {
        NoteQuiz.userInput += noteID;
        userInputList.Add(noteID);
    }

    private IEnumerator Unsubscribe()
    {
        yield return new WaitForSeconds(0.5f);
        PlayNote.OnUserInput -= UpdateUserInput;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayNote.OnUserInput += UpdateUserInput;
        StartCoroutine(Unsubscribe());
    }
}
