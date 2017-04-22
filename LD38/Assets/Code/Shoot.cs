using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Look at mouse
/// </summary>
public class Shoot : MonoBehaviour {
  float timeOfLastShot = -100;
  Bullet bullet;
  Transform bulletSpawn;

	// Use this for initialization
	protected void Start () {
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
      timeOfLastShot = Time.timeSinceLevelLoad;
      var newBullet = Instantiate(bullet);
      newBullet.transform.position = bulletSpawn.transform.position;
      newBullet.shooter = gameObject;
    }
	}

  void Aim()
  {
    var targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane plane = new Plane(Vector3.right, 0);
    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      var position = targetRay.GetPoint(distance);
      var delta =  transform.position - position;
      transform.rotation = Quaternion.LookRotation(delta);
    }
  }
}
