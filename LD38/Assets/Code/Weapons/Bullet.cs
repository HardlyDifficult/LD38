using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Bullet : Projectile
  {
    private void Start()
    {
      body.AddForce(shooter.transform.root.right * speed);
    }
  }
}
