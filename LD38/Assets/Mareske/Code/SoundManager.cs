using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
  public AudioClip crawl;
  public AudioClip explode;
  public AudioClip fireUzi;
  public AudioClip fireRifle;
  public AudioClip fireRocket;

  public static event Action onVolumeChange;
  static AudioClip click;
  static GameObject tAudioSource;
  static AudioSource tSource;

  //Use this 2 variables to adjust the sound ingame
  static float _soundVolume = 1f;
  static float _musicVolume = 1f;

  public static float soundVolume
  {
    get
    {
      return _soundVolume;
    }
    set
    {
      _soundVolume = value;
      if(onVolumeChange != null)
      {
        onVolumeChange.Invoke();
      }
    }
  }
  public static float musicVolume
  {
    get
    {
      return _musicVolume;
    }
    set
    {
      _musicVolume = value;
        Camera.main.GetComponent<AudioSource>().volume = value * .3f;
      if(onVolumeChange != null)
      {
        onVolumeChange.Invoke();
      }
    }
  }

  public static SoundManager instance;

  static void Init()
  {
    click = Resources.Load<AudioClip>("ShortClick");

    tAudioSource = new GameObject();
    DontDestroyOnLoad(tAudioSource);

    tSource = tAudioSource.AddComponent<AudioSource>();
  }

  void Start()
  {
    instance = this;
    Init();
    //DontDestroyOnLoad(transform.gameObject);
  }
  
  #region Public API
  public static void PlayClick()
  {
    Play(click);
  }

  public static void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
  {
    Init();
    tSource.clip = clip;
    tSource.volume = volume * soundVolume;
    tSource.pitch = pitch;

    tAudioSource.transform.position = Camera.main.transform.position;

    tAudioSource.name = clip.name;

    tSource.Play();

  //  GameObject.Destroy(tAudioSource, clip.length);
  }
  #endregion

}
