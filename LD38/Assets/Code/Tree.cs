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
    if(collision.gameObject.GetComponentInChildren<Tree>() != null)
    {
      Destroy(gameObject);
    } else if(Time.timeSinceLevelLoad < .1f && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
    {
      Destroy(gameObject);
    } else 
    {
      if(collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
      {
        Destroy(gameObject);
      }

      SoundManager.Play(headbutt, .2f);
    }
  }
}
