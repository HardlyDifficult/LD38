using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Bullet : Projectile
  {
    protected override  void Start()
    {
      base.Start();

      body.AddForce(-transform.forward * speed);
    }
  }
}
