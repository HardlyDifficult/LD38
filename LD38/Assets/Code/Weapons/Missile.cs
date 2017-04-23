using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Missile : Projectile
  {
    protected override float explosionIntensity
    {
      get
      {
        return 1;
      }
    }

    protected override void Update()
    {
      base.Update();

      body.AddForce(-transform.forward * speed);
    }
  }
}
