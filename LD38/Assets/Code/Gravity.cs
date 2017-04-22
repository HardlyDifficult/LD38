using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  public float force = 10;
  Rigidbody body;
  public bool isGrounded;
  public event Action onGrounded;
  public Quaternion targetRotation;
  public Quaternion turnOffset = Quaternion.identity;

  protected void Awake()
  {
    body = GetComponent<Rigidbody>();
    targetRotation = transform.rotation;
  }

  protected void FixedUpdate()
  {
    Vector3 toCenter = Vector3.zero - transform.position;
    RaycastHit hit;
    if(Physics.Raycast(transform.position + transform.up, toCenter, out hit, Mathf.Infinity,
      LayerMask.GetMask(new[] { "Planet" })))
    {
      Vector3 hitPoint = hit.point - transform.up;
      //transform.position = hit.point;
      var delta = hitPoint - transform.position;
      if(delta.sqrMagnitude < 2f)
      {
        if(isGrounded == false && onGrounded != null)
        {
          onGrounded.Invoke();
        }
        isGrounded = true;
      }
      else
      {
        isGrounded = false;
      }

      if(isGrounded == false)
      {
        body.AddForce(delta.normalized * force);
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
   // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, dot * 50);
    turnOffset = Quaternion.identity;
  }
}
