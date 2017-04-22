using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed;
	
	// Update is called once per frame
	void Update () {
    transform.position += transform.up * speed * -1;
	}

  protected void OnCollisionEnter(
    Collision collision)
  {
    Destroy(collision.transform.gameObject);
  }
}
