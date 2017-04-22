using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

  AudioClip headbutt;

  protected void Awake()
  {
    headbutt = Resources.Load<AudioClip>("HeadbuttTree");
  }

  protected void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.GetComponent<Tree>() != null)
    {
      Destroy(gameObject);
    } else
    {
      SoundManager.Play(headbutt, .2f);
    }
  }
}
