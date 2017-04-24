using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class ShootAssult : Shoot
  {
    public override bool showShootPower
    {
      get
      {
        return false;
      }
    }

    public override float shootPower
    {
      get
      {
        return 10;
      }
    }

    protected override void OnFireStop()
    {
      base.OnFireStop();

      if(shootHoldTime > 0.01f)
      {
        FireProjectile("Bullet", 0);
        TurnController.NextPhase();
      }
    }
  }
}
