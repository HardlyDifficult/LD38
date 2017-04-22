using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  GameObject explosion;
  public float speed;
  internal GameObject shooter;

  protected void Awake()
  {
    explosion = Resources.Load<GameObject>("Explosion");
  }

  void Update()
  {
    transform.Translate((Vector3.forward * speed * Time.deltaTime));
  }

  protected void OnCollisionEnter(
    Collision collision)
  {
    if(collision.gameObject == shooter)
    {
      return;
    }

    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);


    if(collision.gameObject.layer != LayerMask.NameToLayer("Planet"))
    {
      Destroy(collision.transform.gameObject);
    }
    Destroy(gameObject);
  }
}
