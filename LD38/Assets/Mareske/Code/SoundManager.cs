using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //Use this 2 variables to adjust the sound ingame
    public static float soundVolume = 1f;
    public static float musicVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
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

}
