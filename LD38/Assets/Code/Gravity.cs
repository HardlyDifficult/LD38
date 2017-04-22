using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
  const float gravity = 9.8f;
  GameObject planet;
  Rigidbody body;

	protected void Start () {
    body = GetComponent<Rigidbody>();
    planet = GameObject.Find("Planet");
	}
	
	protected void FixedUpdate () {
    var delta = planet.transform.position - transform.position;
    body.AddForce(delta * gravity * Time.fixedTime);

    Ray ray = new Ray(transform.position, delta);
    RaycastHit hit;
    if(Physics.Raycast(ray, out hit))
    {
      transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(new Vector3(90, 0, 0));
    }
	}
}
