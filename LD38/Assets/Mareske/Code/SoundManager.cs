using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  static AudioClip click;
  static GameObject tAudioSource;
  static AudioSource tSource;

  //Use this 2 variables to adjust the sound ingame
  public static float soundVolume = 1f;
  public static float musicVolume = 1f;

  static void Init()
  {
    click = Resources.Load<AudioClip>("ShortClick");

    tAudioSource = new GameObject();
    DontDestroyOnLoad(tAudioSource);

    tSource = tAudioSource.AddComponent<AudioSource>();
  }

  void Awake()
  {
    Init();
    //DontDestroyOnLoad(transform.gameObject);
  }

  /// <summary>
  /// Tunes the volume of the Sound. Send the Audiosource of the object that plays sounds and the rest happens automaticly
  /// </summary>
  /// <param name="_audio"></param>
  public void TuneSoundVolume(AudioSource _audio)
  {
    _audio.volume = _audio.volume * soundVolume;
  }

  /// <summary>
  /// Tunes the volume of the music. Send the Audiosource of the object that plays music and the rest happens automaticly
  /// </summary>
  /// <param name="_audio"></param>
  public void TuneMusicVolume(AudioSource _audio)
  {
    _audio.volume = _audio.volume * musicVolume;
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
