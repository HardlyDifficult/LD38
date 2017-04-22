using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class ShootMissileLauncher : Shoot
  {
    protected Projectile missileResource;

    protected void Awake()
    {
      missileResource = Resources.Load<Projectile>("Missile");
    }

    protected override void OnFireStop()
    {
      base.OnFireStop();

      if(shootHoldTime > 0.01f)
      {
        FireProjectile(missileResource);
        TurnController.NextPhase();
      }
    }
  }
}
