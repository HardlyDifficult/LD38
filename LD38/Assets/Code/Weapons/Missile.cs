using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Missile : Projectile
  {
    protected void Update()
    {
      body.AddForce(transform.right * speed);
    }
  }
}
