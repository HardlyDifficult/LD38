using System.Collections.Generic;
using UnityEngine;



public class SoundPool : MonoBehaviour
{
    public struct SoundObject
    {
        public SoundClip Clip;
        public AudioSource Source;
    }

    private List<AudioSource> _sources;
    private List<SoundObject> _currentlyPlaying;

    public void Initialize(int size)
    {
        DontDestroyOnLoad(gameObject);

        _sources = new List<AudioSource>(size);
        _currentlyPlaying = new List<SoundObject>(size);

        for (int i = 0; i < size; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            _sources.Add(source);
        }
    }

    private void FixedUpdate()
    {
        if (_currentlyPlaying != null && _currentlyPlaying.Count > 0)
        {
            for (int i = 0; i < _currentlyPlaying.Count; i++)
            {
                if (!_currentlyPlaying[i].Source.isPlaying)
                {
                    _currentlyPlaying.Remove(_currentlyPlaying[i]);
                }
            }
        }
    }

    public void UpdateVolume(float amount)
    {
        for (int i = 0; i < _currentlyPlaying.Count; i++)
        {
            SoundClip sound = _currentlyPlaying[i].Clip;
            AudioSource source = _currentlyPlaying[i].Source;

            source.volume = sound.BaseVolume * amount;
        }
    }

    public void UpdatePitch(float amount)
    {
        for (int i = 0; i < _currentlyPlaying.Count; i++)
        {
            SoundClip sound = _currentlyPlaying[i].Clip;
            AudioSource source = _currentlyPlaying[i].Source;

            source.pitch = sound.BasePitch * amount;
        }
    }

    public void Play(SoundClip clip, float volume, float pitch)
    {
        SoundObject soundObject= new SoundObject();
        soundObject.Source = GetSource(clip, volume, pitch);
        soundObject.Clip = clip;
        soundObject.Source.Play();

        _currentlyPlaying.Add(soundObject);
    }

    private AudioSource GetSource(SoundClip clip, float volume, float pitch)
    {
        AudioSource source = null;
        for (int i = 0; i < _sources.Count; i++)
        {
            if(_sources[i] == null) continue;
            if(_sources[i].isPlaying) continue;

            source = _sources[i];
            source.clip = clip.UnityClip;
            source.volume = clip.BaseVolume * volume;
            source.pitch = clip.BasePitch*pitch;

            return source;
        }

        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip.UnityClip;
        source.volume = clip.BaseVolume * volume;
        source.pitch = clip.BasePitch * pitch;

        return source;
    }
}

