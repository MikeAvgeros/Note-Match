using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayRemainingNotes : MonoBehaviour
{
    public TextMeshProUGUI notesText;

    private void Start()
    {
        StartCoroutine(CountRemainingNotes());
    }

    private IEnumerator CountRemainingNotes()
    {
        while (true)
        {
            notesText.text = NoteQuiz.notesLeft.ToString();
            yield return null;
        }
    }
}
