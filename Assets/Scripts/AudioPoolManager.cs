using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPoolManager : MonoBehaviour
{
    public static AudioPoolManager instance;
    public static AudioPoolManager Instance { get { return instance; } }

    public List<AudioSource> audioSourcePool = new List<AudioSource>();

    public AudioMixerGroup noteMixerGroup;

    public AudioMixerGroup sFXMixerGroup;

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayNoteSound(NoteData note)
    {
        var audioSource = GetAudioSource();
        audioSource.loop = false;
        Play(audioSource, note);
    }

    public void PlayUISound(AudioClip audioClip)
    {
        var audioSource = GetAudioSource();
        audioSource.loop = false;
        PlayUI(audioSource, audioClip);
    }

    private void Play(AudioSource audioSource, NoteData note)
    {
        audioSource.clip = note.noteSound;
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = noteMixerGroup;
        audioSource.Play();
    }

    private void PlayUI(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = sFXMixerGroup;
        audioSource.Play();
    }

    public void StopNoteSound(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / 1f;
            }
            audioSource.Stop();
        }
    }

    private AudioSource GetAudioSource()
    {
        var audioSource = audioSourcePool.FirstOrDefault(x => !x.isPlaying);
        if (audioSource == null)
        {
            audioSource = CreateAudioSource();
            audioSourcePool.Add(audioSource);
        }
        return audioSource;
    }

    private AudioSource CreateAudioSource()
    {
        var go = new GameObject("AudioSource", typeof(AudioSource));
        go.transform.SetParent(transform);
        var audioSource = go.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }
}
