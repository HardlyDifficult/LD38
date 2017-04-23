using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
  public float speed = 1;
  public float rotationSpeed = 10;
  const float Zoom = 10;

  protected void Update()
  {
    //var targetLocation = TurnController.currentWorm.transform.position;
    //targetLocation += TurnController.currentWorm.transform.rotation * new Vector3(0, 5, 10000);
    //transform.position = Vector3.Lerp(transform.position, targetLocation, speed);



    if(TurnController.currentWorm == null)
    {
      return;
    }

    Vector3 offset = TurnController.currentWorm.transform.up * Zoom + -TurnController.currentWorm.transform.forward * Zoom;
    Vector3 position = TurnController.currentWorm.transform.position + offset;

    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    transform.rotation = Quaternion.LookRotation(TurnController.currentWorm.transform.position - transform.position,
      TurnController.currentWorm.transform.up);
    //transform.LookAt(TurnController.currentWorm.transform);
  }
}
