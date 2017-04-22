using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoot : MonoBehaviour
{
  protected TeamPlayer teamPlayer;
  protected Transform bulletSpawnAnchorPointOnGun;
  protected float shootHoldTime;

  protected void Start()
  {
    teamPlayer = transform.root.GetComponent<TeamPlayer>();
    bulletSpawnAnchorPointOnGun = transform.FindChild("BulletSpawn");
  }

  protected virtual void OnFireStart()
  {

  }

  protected virtual void OnFireStay()
  {

  }

  protected virtual void OnFireStop()
  {

  }

  protected void Update()
  {
    if(teamPlayer.isMyTurn == false || TurnController.phase != TurnController.Phase.Shoot)
    {
      return;
    }

    Aim();

    if(Input.GetAxis("Fire1") > 0)
    {
      if(shootHoldTime == 0)
      {
        OnFireStart();
      }
      OnFireStay();
      shootHoldTime += Time.deltaTime;
    }
    else 
    {
      OnFireStop();
      shootHoldTime = 0;
    }
  } 

  void Aim()
  {
    Ray targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane plane = new Plane(transform.root.forward, transform.root.position);
    
    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      Vector3 mousePosition = targetRay.GetPoint(distance);
     
      Vector3 delta =  transform.position - mousePosition;
      Vector3 up = transform.position - Vector3.zero;

      transform.rotation = Quaternion.LookRotation(delta, up);
    }
  }

  protected void FireProjectile(Projectile resource)
  {
    Projectile newBullet = Instantiate(resource, bulletSpawnAnchorPointOnGun.transform.position, transform.rotation);
    newBullet.shooter = gameObject.transform.root.gameObject;
    newBullet.shootHoldTime = shootHoldTime;
  }
}
