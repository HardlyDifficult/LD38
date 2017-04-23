using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  public float force = 10;
  Rigidbody body;
  public bool isGrounded = true;
  public event Action onGrounded;
  public Quaternion targetRotation;
  public Quaternion turnOffset = Quaternion.identity;

  protected void Start()
  {
    body = GetComponent<Rigidbody>();
    targetRotation = transform.rotation;
  }

  protected void FixedUpdate()
  {
    Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;
    RaycastHit hit;

    Vector3 objectPivotPointPlusOne = transform.position - directionToCenter;
    if(Physics.Raycast(objectPivotPointPlusOne, directionToCenter, out hit, Mathf.Infinity,
      LayerMask.GetMask(new[] { "Planet" })))
    {
      Vector3 hitPoint = hit.point + directionToCenter;
      var delta = hitPoint - transform.position;
      if(delta.sqrMagnitude < 2f)
      {
        if(isGrounded == false && onGrounded != null)
        {
          onGrounded.Invoke();
        }
        isGrounded = true;
        body.AddForce(delta.normalized * force);

      }
      else if(delta.sqrMagnitude < .1f)
      {
        isGrounded = false;
      } else
      {
        body.AddForce(delta.normalized * force);
        isGrounded = false;
      }

      targetRotation = Quaternion.FromToRotation(targetRotation * Vector3.up,
        hit.normal
        // old transform.position - Vector3.zero
        ) * targetRotation;
    }
  }

  protected void Update()
  {
    transform.rotation *= turnOffset;
    targetRotation *= turnOffset;
    var dot = 1 - Quaternion.Dot(transform.rotation, targetRotation);
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, dot * 50);
    turnOffset = Quaternion.identity;
  }
}
