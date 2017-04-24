using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
  public float speed = 10;
  internal GameObject shooter;
  internal float shootPower;
  bool isDead;

  protected Gravity gravity;
  //protected ExplosionDamage explosion;
  protected Vector3 previousPosition;
  protected Rigidbody body;

  protected abstract float explosionIntensity
  {
    get;
  }

  protected virtual void Awake()
  {
    //explosion = Resources.Load<ExplosionDamage>("");
  }

  protected virtual void Start()
  {
    body = GetComponent<Rigidbody>();
    gravity = GetComponent<Gravity>();
    gravity.allowRotation = false;
    gravity.onGrounded += BlowUp;

    previousPosition = transform.position;
  }

  private void OnDestroy()
  {
    isDead = true;
  }

  protected virtual void OnCollisionEnter(
    Collision collision)
  {

    if(PhotonView.Get(this).isMine == false)
    {
      return;
    }

    if(collision.transform.root == shooter)
    { // TODO this shouldn't really be here - just trying to avoid collisions coming out of the gun.
      return;
    }
    
    BlowUp();
  }

  protected virtual void Update()
  {
    Vector3 delta = previousPosition - transform.position;
    if(delta.sqrMagnitude > .1f)
    {
      Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;
      transform.rotation = Quaternion.LookRotation(delta, directionToCenter);
      previousPosition = transform.position;
    }
  }

  protected void BlowUp()
  {

    if(isDead || PhotonView.Get(this).isMine == false)
    {
      return;
    }

    isDead = true;
    var newExplosion = PhotonNetwork.Instantiate("Explosion3", transform.position, Quaternion.identity, 0);
    newExplosion.transform.localScale = Vector3.one * explosionIntensity;
    PhotonNetwork.Destroy(gameObject);
  }
}
