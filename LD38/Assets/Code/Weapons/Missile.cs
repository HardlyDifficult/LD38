using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  /// <summary>
  /// When holding / charging display UI bar
  /// Right click to cancel a shot
  /// </summary>
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

      body.AddForce(-transform.forward * speed * shootPower * Time.deltaTime);
      speed *= .99f;
    }
  }
}
