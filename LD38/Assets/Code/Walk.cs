using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
  public float speed = .1f; 
  Rigidbody body;
  GameObject planet;

  protected void Start()
  {
    body = GetComponent<Rigidbody>();
    planet = GameObject.Find("Planet");
  }


  protected void Update()
  {
    var horizontal = Input.GetAxis("Horizontal");
    if(horizontal == 0) {
      return;
    }
    var delta =  planet.transform.position - transform.position;
    var rotation = Quaternion.Euler(delta) * Quaternion.Euler(new Vector3(90, 0, 0));

    transform.position +=  rotation * Vector3.right * horizontal;

//    var direction = Quaternion.Euler(Vector3.up) * delta;
////    transform.position += direction * speed * horizontal;

//    Ray ray = new Ray(transform.position, delta * -1);
//    RaycastHit hit;
//    if(Physics.Raycast(ray, out hit))
//    {
//      transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(new Vector3(90, 0, 0));
//    //  transform.position += transform.rotation * Vector3.right * speed * horizontal;
//    }
  }
}
