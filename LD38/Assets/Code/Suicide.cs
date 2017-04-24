using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour {
  public float timeToLiveInSeconds = 100;
  
	// Update is called once per frame
	void Update () {

    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }
    timeToLiveInSeconds -= Time.deltaTime;
    if(timeToLiveInSeconds <= 0)
    {
      PhotonNetwork.Destroy(gameObject);
    }
	}
}
