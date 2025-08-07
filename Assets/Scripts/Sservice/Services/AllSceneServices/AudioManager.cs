using UnityEngine;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Sservice;

public class AudioManager : MonoBehaviour ,IService 
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClipsData audioClipData;

    private Dictionary<AudioClipType, AudioClip> _audioClips;
    

    private void LoadAudioClips()
    {
        _audioClips = new Dictionary<AudioClipType, AudioClip>();
        foreach (var entry in audioClipData.clips)
        {
            if (!_audioClips.ContainsKey(entry.type))
            {
                _audioClips.Add(entry.type, entry.clip);
            }
        }
    }

    public void PlaySfx(AudioClipType type)
    {
        if (_audioClips.TryGetValue(type, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect of type {type} not found.");
        }
    }

    public void PlayMusic(AudioClipType type, bool loop = true)
    {
        if (_audioClips.TryGetValue(type, out var clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music of type {type} not found.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void Load()
    {
        LoadAudioClips();
    }
}