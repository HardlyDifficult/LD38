using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
  float timeOfLastShot = -100;
  Bullet bullet;
  Transform bulletSpawn;
  GameObject planet;
  float shootHoldTime;

	// Use this for initialization
	protected void Start () {
    planet = GameObject.Find("Planet");
    bullet = Resources.Load<Bullet>("Bullet");
    bulletSpawn = transform.FindChild("BulletSpawn");
	}
	
	// Update is called once per frame
	void Update () {
    
    Aim();

    if(Time.timeSinceLevelLoad - timeOfLastShot < 1)
    {
      return;
    }

		if(Input.GetAxis("Fire1") > 0)
    {
      shootHoldTime += Time.deltaTime;
    } else if(shootHoldTime > 0)
    {
      timeOfLastShot = Time.timeSinceLevelLoad;
      var newBullet = Instantiate(bullet);
      newBullet.transform.position = bulletSpawn.transform.position;
      newBullet.transform.rotation = transform.rotation;
      newBullet.shooter = gameObject.transform.parent.gameObject;
      newBullet.speed *= shootHoldTime;
      shootHoldTime = 0;
    }
	}

  void Aim()
  {
    var targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane plane = new Plane(Vector3.right, 0);
    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      var mousePosition = targetRay.GetPoint(distance);
      var delta =  transform.position - mousePosition;
      var up = transform.position - planet.transform.position;
      transform.rotation = Quaternion.LookRotation(delta, up);
    }
  }
}
