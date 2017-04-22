using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
  float timeOfLastShot = -100;
  Bullet bullet;

	// Use this for initialization
	protected void Start () {
    bullet = Resources.Load<Bullet>("Bullet");
	}
	
	// Update is called once per frame
	void Update () {
    if(Time.timeSinceLevelLoad - timeOfLastShot < 1)
    {
      return;
    }

		if(Input.GetAxis("Fire1") > 0)
    {
      timeOfLastShot = Time.timeSinceLevelLoad;
      var newBullet = Instantiate(bullet);
      newBullet.transform.position += transform.position;
      newBullet.shooter = gameObject;
    }
	}
}
