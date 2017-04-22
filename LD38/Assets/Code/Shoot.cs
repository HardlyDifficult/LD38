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
      newBullet.shooter = gameObject.transform.parent.gameObject;
      newBullet.speed *= shootHoldTime;
      shootHoldTime = 0;
      //TurnController.currentTeam++;
      TurnController.NextPhase();
    }
  }

  void Aim()
  {
    var targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane plane = new Plane(Vector3.forward, 0);
    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      var mousePosition = targetRay.GetPoint(distance);
      var delta = transform.position - mousePosition;
      var up = transform.position - Vector3.zero;
      transform.rotation = Quaternion.LookRotation(delta, up);
    }
  }
}
