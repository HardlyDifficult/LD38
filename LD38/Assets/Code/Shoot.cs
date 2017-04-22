using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  TeamPlayer teamPlayer;

  float timeOfLastShot = -100;
  Bullet bullet;
  Transform bulletSpawn;
  float shootHoldTime;

  protected void Start()
  {
    teamPlayer = transform.root.GetComponent<TeamPlayer>();
    bullet = Resources.Load<Bullet>("Bullet");
    bulletSpawn = transform.FindChild("BulletSpawn");
  }

  protected void Update()
  {
    if(teamPlayer.isMyTurn == false || TurnController.phase != TurnController.Phase.Shoot)
    {
      return;
    }

    Aim();

    if(Time.timeSinceLevelLoad - timeOfLastShot < 1)
    {
      return;
    }

    if(Input.GetAxis("Fire1") > 0)
    {
      shootHoldTime += Time.deltaTime;
    }
    else if(shootHoldTime > 0.01f)
    {
      timeOfLastShot = Time.timeSinceLevelLoad;
      var newBullet = Instantiate(bullet);
      newBullet.transform.position = bulletSpawn.transform.position;
      newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y);
      newBullet.transform.rotation = transform.rotation;
      newBullet.shooter = gameObject.transform.root.gameObject;
      newBullet.speed *= shootHoldTime;
      shootHoldTime = 0;
      //TurnController.currentTeam++;
      TurnController.NextPhase();
    }
  }

  void Aim()
  {
    Ray targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane plane = new Plane(transform.root.forward, transform.root.position);

    Debug.DrawRay(transform.position, transform.root.right * 10000);
    Debug.DrawRay(transform.position, -transform.root.right * 10000);

    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      Vector3 mousePosition = targetRay.GetPoint(distance);
     
      Vector3 delta =  transform.position - mousePosition;
      Vector3 up = transform.position - Vector3.zero;

      transform.rotation = Quaternion.LookRotation(delta, up);
    } 
  }
}
