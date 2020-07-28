using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "Music Game/Note")]
public class NoteData : ScriptableObject
{
    public AudioClip noteSound;
    public string noteName;
    public int ID;
    public string[] isInKeys;
}
