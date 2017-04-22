using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
    GetComponent<AudioSource>().volume *= SoundManager.musicVolume;
	}
}
