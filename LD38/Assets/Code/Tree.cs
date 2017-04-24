using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

  AudioClip headbutt;
  public GameObject deadTree;

  protected void Start()
  {
    headbutt = Resources.Load<AudioClip>("HeadbuttTree");
  }

  protected void OnDestroy()
  {
    var go = Instantiate(deadTree, transform.position, transform.rotation);
    go.transform.localScale = transform.root.localScale;
  }

  protected void OnCollisionEnter(Collision collision)
  {
    SoundManager.Play(headbutt, .2f);
    if(PhotonView.Get(gameObject.transform.root.gameObject).isMine == false)
    {
      return;
    }

    if(collision.gameObject.GetComponentInChildren<Tree>() != null)
    {
      PhotonNetwork.Destroy(gameObject);
    }
    else if(Time.timeSinceLevelLoad < .1f && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
    {
      PhotonNetwork.Destroy(gameObject);
    }
    else
    {
      if(collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
      {
        PhotonNetwork.Destroy(gameObject.transform.root.gameObject);
      }

    }
  }
}
