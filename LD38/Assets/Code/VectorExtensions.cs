using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public static class VectorExtensions
{
  public static Vector3 Times(this Vector3 a, Vector3 b)
  {
    return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
  }
}
