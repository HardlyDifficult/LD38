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

  protected void Awake()
  {
    body = GetComponent<Rigidbody>();
  }

  protected void FixedUpdate()
  {
    Vector3 toCenter = Vector3.zero - transform.position;
    RaycastHit hit;
    if(Physics.Raycast(transform.position + transform.up, toCenter, out hit, Mathf.Infinity,
      LayerMask.GetMask(new[] { "Planet" })))
    {
      //transform.position = hit.point;
      var delta = hit.point - transform.position;
      if(delta.sqrMagnitude < .01f)
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

      transform.rotation = Quaternion.FromToRotation(transform.up, transform.position -
        Vector3.zero) * transform.rotation;
    } 
  }
}
