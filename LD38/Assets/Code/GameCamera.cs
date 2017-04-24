using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
  public float speed = 1;
  public float rotationSpeed = 10;
  const float Zoom = 10;

  protected void Update()
  {
    if(TurnController.CurrentPlayer == null)
      return;

    Vector3 offset = TurnController.CurrentPlayer.transform.up * Zoom + -TurnController.CurrentPlayer.transform.forward * Zoom;
    Vector3 position = TurnController.CurrentPlayer.transform.position + offset;

    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    transform.rotation = Quaternion.LookRotation(TurnController.CurrentPlayer.transform.position - transform.position,
      TurnController.CurrentPlayer.transform.up);
  }




//public class SphereCamera : MonoBehaviour
//{

//Transform _rotateCenter;
//  public Transform rotateCenter
//  {
//    get
//    {
//      if(_rotateCenter == null)
//      {
//        if(GameObject.Find("Planet") == null)
//        {
//          return null;
//        }
//        _rotateCenter = GameObject.Find("Planet").transform;
//      }

//      return _rotateCenter;
//    }
//  }
//  public Transform rotatePoint
//  {
//    get
//    {
//      if(TurnController.CurrentPlayer == null)
//      {
//        return GameObject.Find("Planet").transform;
//      }
//      return TurnController.CurrentPlayer.transform;
//    }
//  }

//  public float distance
//  {
//    get
//    {
//      return planet.transform.localScale.x + 10;
//    }
//  }

//  public float[] angles;

//  GameObject planet;

//  private void Start()
//  {
//    planet = GameObject.Find("Planet");
//  }

//  void Update()
//  {
//    float[] angles = calcAngle(rotatePoint.position - rotateCenter.position);
//    this.transform.position = Vector3.Slerp(this.transform.position, rotateCenter.position + calculateCameraPosition(-angles[0], -angles[1]), 0.1f);
//    if(TurnController.CurrentPlayer != null)
//    {
//      var deltaPosition = TurnController.CurrentPlayer.transform.position - transform.position;
//      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(deltaPosition, transform.up), 1f * Time.deltaTime);
//      //transform.LookAt(TurnController.CurrentPlayer.transform);
//    }
//  }

//  float[] calcAngle(Vector3 objectPos)
//  {
//    float[] angles = { 0, 0 };
//    angles[0] = Vector3.Angle(Vector3.forward, objectPos.normalized) - 90;
//    angles[1] = Vector3.Angle(Vector3.up, objectPos.normalized) - 90;

//    if(objectPos.normalized.x < 0)
//    {
//      angles[0] = -angles[0] - 180;
//    }

//    this.angles = angles;
//    return angles;
//  }

//  Vector3 calculateCameraPosition(float rotX, float rotY)
//  {
//    rotX *= Mathf.Deg2Rad;
//    rotY *= Mathf.Deg2Rad;

//    return new Vector3(
//      distance * Mathf.Cos(rotX) * Mathf.Cos(rotY),
//      distance * Mathf.Sin(rotY),
//      distance * Mathf.Sin(rotX) * Mathf.Cos(rotY));
//  }

//  void OnDrawGizmos()
//  {
//    Gizmos.color = Color.red;
//    Gizmos.DrawWireSphere(rotateCenter.position, distance);
//    Gizmos.DrawLine(this.transform.position, rotateCenter.position);
//  }
}