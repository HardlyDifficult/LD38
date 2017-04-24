using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLine : MonoBehaviour
{
  float _life = 100;

  public float life
  {
    get
    {
      return _life;
    }
    set
    {
      GetComponent<PhotonView>().RPC("SetLife", PhotonTargets.All, value);
    }
  }

  [PunRPC]
  void SetLife(float value)
  {
    _life = value;

    if(life <= 0)
    {

      if(PhotonView.Get(this).isMine == false)
      {
        return;
      }

      PhotonNetwork.Destroy(gameObject);
    }
  }
}
