using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

  AudioClip headbutt;

  protected void Start()
  {
    headbutt = Resources.Load<AudioClip>("HeadbuttTree");
  }

  protected void OnCollisionEnter(Collision collision)
  {

      SoundManager.Play(headbutt, .2f);
    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }

    if(collision.gameObject.GetComponentInChildren<Tree>() != null)
    {
      PhotonNetwork.Destroy(gameObject);
    } else if(Time.timeSinceLevelLoad < .1f && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
    {
      PhotonNetwork.Destroy(gameObject);
    } else 
    {
      if(collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
      {
        PhotonNetwork.Destroy(gameObject);
      }

    }
  }
}
