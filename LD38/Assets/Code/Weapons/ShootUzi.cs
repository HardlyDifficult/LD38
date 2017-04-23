using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class ShootUzi : Shoot
  {
    public int bulletCount = 42;
    int bulletsInChamber;

    protected Projectile bulletResource;

    protected override  void Start()
    {
      base.Start();

      bulletResource = Resources.Load<Projectile>("Bullet");
      bulletsInChamber = bulletCount;

      TurnController.onTurnChange += TurnController_onTurnChange;
    }

    void TurnController_onTurnChange()
    {
      bulletsInChamber = bulletCount;
    }

    protected override void OnFireStay()
    {
      base.OnFireStay();

      FireProjectile(bulletResource, 1);

      if(bulletsInChamber-- <= 0)
      {
        TurnController.NextPhase();
      }
    }
  }
}
