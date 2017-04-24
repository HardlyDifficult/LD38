using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLine : MonoBehaviour {
  float _life = 100;

  public float life
  {
    get
    {
      return _life;
    }
    set
    {
      _life = value;

      if(life <= 0)
      {

        if(PhotonNetwork.isMasterClient == false)
        {
          return;
        }

        PhotonNetwork.Destroy(gameObject);
      }
    }
  }
}
