using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
  public float speed = 10;
  internal GameObject shooter;
  internal float shootHoldTime;

  protected Gravity gravity;
  protected GameObject explosion;
  protected Vector3 previousPosition;
  protected Rigidbody body;

  protected virtual void Start()
  {
    body = GetComponent<Rigidbody>();
    gravity = GetComponent<Gravity>();
    gravity.onGrounded += BlowUp;
    explosion = Resources.Load<GameObject>("Explosion");
    previousPosition = transform.position;
  }

  protected virtual void OnCollisionEnter(
    Collision collision)
  {
    if(collision.transform.root == shooter)
    { // TODO this shouldn't really be here - just trying to avoid collisions coming out of the gun.
      return;
    }
    
    BlowUp();
  }

  protected void BlowUp()
  {
    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
