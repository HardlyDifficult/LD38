using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Bullet : Projectile
  {
    protected override float explosionIntensity
    {
      get
      {
        return .1f * shootPower;
      }
    }

    protected override void Start()
    {
      base.Start();
      if(shootPower < 1)
      {
        transform.localScale = new Vector3(.15f, .15f, .15f);
      }
      else
      {
        transform.localScale = new Vector3(.4f, .4f, .4f);
      }
      body.AddForce(-transform.forward * speed);
    }
  }
}
