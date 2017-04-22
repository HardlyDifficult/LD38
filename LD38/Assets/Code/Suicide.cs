using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour {
  public float timeToLiveInSeconds = 100;
  
	// Update is called once per frame
	void Update () {
    timeToLiveInSeconds -= Time.deltaTime;
    if(timeToLiveInSeconds <= 0)
    {
      Destroy(gameObject);
    }
	}
}
