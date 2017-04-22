using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

  public class Gravity2 : MonoBehaviour
  {
    public float force = 10;
    Rigidbody body;
    public bool isGrounded;

    protected void Awake()
    {
      body = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
//      isGrounded = false;

      Vector3 toCenter = Vector3.zero - transform.position;
      RaycastHit hit;
      if(Physics.Raycast(transform.position, toCenter, out hit, Mathf.Infinity, 
        LayerMask.GetMask(new [] {"Planet"})))
      {
        //transform.position = hit.point;
        var delta = hit.point - transform.position;
        if(delta.sqrMagnitude < .01f)
        {
          isGrounded = true;
          return;
        } else
      {
        isGrounded = false;
      }
        body.AddForce(delta.normalized * force);

        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - 
          Vector3.zero) * transform.rotation;
      }
    }
  }
