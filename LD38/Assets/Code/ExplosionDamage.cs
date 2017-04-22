using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {
  float baseDamage = 100000;
  float colliderRadius;

  protected void Awake()
  {
    colliderRadius = GetComponent<SphereCollider>().radius;
    StartCoroutine(SuicideScript());
    PlanetDeformation.instance.ExplodeAt(transform.position, 3);
  }

  private IEnumerator SuicideScript()
  {
    yield return new WaitForSeconds(.1f);
    Destroy(this);
  }

  protected void OnTriggerEnter(
    Collider other)
  {
    var lifeLine = other.GetComponent<LifeLine>();
    if(lifeLine == null)
    {
      return;
    }
    
    var percentDamage = other.contactOffset / colliderRadius;
    lifeLine.life -= percentDamage * baseDamage;
  }
}
