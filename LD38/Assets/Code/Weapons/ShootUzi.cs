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

    public override float shootPower
    {
      get
      {
        return 1;
      }
    }

    public override bool showShootPower
    {
      get
      {
        return false;
      }
    }

    protected override  void Start()
    {
      base.Start();

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

      FireProjectile("Bullet", 10);

      if(bulletsInChamber-- <= 0)
      {
        TurnController.NextPhase();
      }
    }
  }
}
