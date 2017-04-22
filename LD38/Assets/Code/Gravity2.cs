using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace HD
{
  public class Gravity2 : MonoBehaviour
  {

    protected void Update()
    {
      Vector3 toCenter = Vector3.zero - transform.position;
      RaycastHit hit;
      if(Physics.Raycast(transform.position, toCenter, out hit, Mathf.Infinity, 
        LayerMask.GetMask(new [] {"Planet"})))
      {
        transform.position = hit.point;
        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - 
          Vector3.zero) * transform.rotation;
      }
    }
  }
}
