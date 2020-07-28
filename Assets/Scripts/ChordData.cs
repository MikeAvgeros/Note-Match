using UnityEngine;

[CreateAssetMenu(fileName = "Chord", menuName = "Music Game/Chord")]
public class ChordData : ScriptableObject
{
    public NoteData[] notesInChord;
    public string chordName;
    public bool isMajor;
}
