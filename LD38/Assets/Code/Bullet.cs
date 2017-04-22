using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  Gravity gravity;
  GameObject explosion;
  public float speed;
  internal GameObject shooter;
  Vector3 previousPosition;

  protected void Awake()
  {
    gravity = GetComponent<Gravity>();
    gravity.onGrounded += BlowUp;
    explosion = Resources.Load<GameObject>("Explosion");
    previousPosition = transform.position;
  }

  void Update()
  {
    transform.Translate((Vector3.back * speed * Time.deltaTime));
    
    var delta = previousPosition - transform.position;
    if(delta.sqrMagnitude > .1)
    {
      var up = transform.position - Vector3.zero;
      transform.rotation = Quaternion.LookRotation(delta, up);
    }
  }

  private void LateUpdate()
  {
    previousPosition = transform.position;
  }

  private void OnCollisionEnter(
    Collision collision)
  {
    if(collision.transform.root == shooter)
    {
      return;
    }
    
    //BlowUp();
  }

  private void BlowUp()
  {
    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
