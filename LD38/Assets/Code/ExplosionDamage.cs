using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {
  float baseDamage = 100000;
  float colliderRadius;

  protected void Start()
  {
    if(PhotonNetwork.isMasterClient == false)
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
    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }
    var lifeLine = other.GetComponent<LifeLine>();
    if(lifeLine == null)
    {
      return;
    }
    
    var percentDamage = other.contactOffset / colliderRadius;
    lifeLine.life -= percentDamage * baseDamage;
  }
}
