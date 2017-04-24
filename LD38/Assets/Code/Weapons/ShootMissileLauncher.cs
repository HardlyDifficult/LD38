using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class ShootMissileLauncher : Shoot
  {

    public override bool showShootPower
    {
      get
      {
        return true;
      }
    }
    

    protected override void OnFireStop()
    {
      base.OnFireStop();

      if(shootHoldTime > 0.01f)
      {
        FireProjectile("Missile", .1f);
        SoundManager.Play(SoundManager.instance.fireRocket, .1f);

        TurnController.NextPhase();
      }
    }
  }
}
