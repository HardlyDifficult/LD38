using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed;
  internal GameObject shooter;

  // Update is called once per frame
  void Update () {
    transform.Translate((Vector3.forward * speed * Time.deltaTime));
	}

  protected void OnCollisionEnter(
    Collision collision)
  {
    if(collision.gameObject == shooter)
    {
      return;
    }

    Destroy(collision.transform.gameObject);
    Destroy(gameObject);
  }
}
