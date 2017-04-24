using System;
using UnityEngine;

public enum SoundType
{
    Effect,
    Music,
}

[Serializable]
public struct SoundClip
{
    public string Name;
    public float BaseVolume;
    public float BasePitch;
    public AudioClip UnityClip;

    public SoundClip(string name, float baseVolume, float basePitch, AudioClip unityClip)
    {
        Name = name;
        BaseVolume = baseVolume;
        BasePitch = basePitch;
        UnityClip = unityClip;
    }
}

public delegate void VolumeChanged(float vol);

public class SoundManager : MonoBehaviour
{
    public VolumeChanged MusicVolumeChanged;
    public VolumeChanged EffectsVolumeChanged;

    private float _musicVolume = 1.0f;
    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = Mathf.Clamp01(value);

            if(MusicVolumeChanged != null)
                MusicVolumeChanged.Invoke(_musicVolume);

            MusicSoundPool.UpdateVolume(_musicVolume);
        }
    }

    private float _effectVolume = 1.0f;
    public float EffectVolume
    {
        get { return _effectVolume; }
        set
        {
            _effectVolume = Mathf.Clamp01(value);

            if (EffectsVolumeChanged != null)
                EffectsVolumeChanged.Invoke(_effectVolume);

            EffectSoundPool.UpdateVolume(_effectVolume);
        }
    }

    public SoundPool EffectSoundPool;
    public SoundPool MusicSoundPool;
    public SoundLibrary SoundLib;

    private void Awake()
    { 
        if(Game.SoundManager != null)
            Destroy(gameObject);

        Game.SoundManager = this;

        DontDestroyOnLoad(gameObject);

        SoundLib = GetComponent<SoundLibrary>();

        EffectSoundPool = gameObject.AddComponent<SoundPool>();
        EffectSoundPool.Initialize(5);

        MusicSoundPool = gameObject.AddComponent<SoundPool>();
        MusicSoundPool.Initialize(1);

        //Background music loop
    }

    //TODO: Support per-sound volume and pitch
    public void PlayMusic(SoundClip clip)
    {
        MusicSoundPool.Play(clip, MusicVolume, 1.0f);
    }

    public void PlayEffect(SoundClip clip)
    {
        EffectSoundPool.Play(clip, EffectVolume, 1.0f);
    }

    public SoundClip? GetSoundClip(string name)
    {
        if (SoundLib != null)
        {
            SoundClip? clip = SoundLib.GetByName(name);
            if (clip.HasValue)
            {
                return clip.Value;
            }
            else
            {
                Debug.LogWarning("Could not find soundclip");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("There is no sound library available");
            return null;
        }

    }


}
