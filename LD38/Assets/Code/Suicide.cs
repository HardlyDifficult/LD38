using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{

  public bool isNotPhoton;
  public float timeToLiveInSeconds = 100;

  // Update is called once per frame
  void Update()
  {

    if(isNotPhoton == false && PhotonView.Get(this).isMine == false)
    {
      return;
    }
    timeToLiveInSeconds -= Time.deltaTime;
    if(timeToLiveInSeconds <= 0)
    {
      if(isNotPhoton == false)
      {
        PhotonNetwork.Destroy(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }
}
