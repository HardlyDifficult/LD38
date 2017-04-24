using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {
  float baseDamage = 1000;
  float colliderRadius;

  protected void Start()
  {
    if(PhotonView.Get(this).isMine == false)
    {
      return;
    }

    colliderRadius = GetComponent<SphereCollider>().radius;
    StartCoroutine(SuicideScript());
    // Does not leave a valid Mesh behind... PlanetDeformation.instance.ExplodeAt(transform.position, 3);
  }

  private IEnumerator SuicideScript()
  {
    yield return new WaitForSeconds(.1f);
    Destroy(this);
  }

  protected void OnTriggerEnter(
    Collider other)
  {
    if(PhotonView.Get(this).isMine == false)
    {
      return;
    }
    var lifeLine = other.GetComponent<LifeLine>();
    if(lifeLine == null)
    {
      return;
    }
    
    var percentDamage = Mathf.Min(1, 100 * other.contactOffset / colliderRadius);
    lifeLine.life -= percentDamage * baseDamage;
  }
}
