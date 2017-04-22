using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
  public bool disableRotation;

  float fallSpeed = .050f; //How strong the gravity is, gets multiplied by the distance to the surface
  public bool isGrounded = false; //Checks if we can walk on the surface under us
  LayerMask rayLayer; //What we check the raycast against
  float velocity;
  public event Action onGrounded;

  private void Awake()
  {
    rayLayer = LayerMask.GetMask(new[] { "Planet" });
  }

  // Update is called once per frame
  void Update()
  {
    CheckGravity();
  }

  /// <summary>
  /// Checks if we are in the air and need to fall
  /// </summary>
  private void CheckGravity()
  {
    //Hardly is a handsome guy and everyone who can should leave him some bits or sweet donations
    //For all the entertaiment he gets us :-)

    //Storing the distance to the ground for later usage
    float distanceToGround = 0;
    //Also the raycasthit
    RaycastHit hit;

    //The raycast starts under us
    Vector3 down = Vector3.zero - transform.position;  //transform.TransformDirection(Vector3.down);

    //We start the raycast
    if(Physics.Raycast(transform.position, down, out hit, Mathf.Infinity, rayLayer))
    {
      //We store the distance to the ground for later usage
      distanceToGround = Vector3.Distance(hit.point, transform.position);

      //if we are near the surface then we are grounded and dont fall anymore
      if(distanceToGround <= 0.1f)
      {
        isGrounded = true;
        velocity = 0;
        if(onGrounded != null)
        {
          onGrounded.Invoke();
        }
      }

      //If the worm is not grounded then we pull him down to fall
      // if(!isGrounded)
      {
        //Calculating new fallspeed, to fall faster the further we are from the surface
        float newFallspeed = fallSpeed * distanceToGround;
        //If the fallspeed is slower then the main fallspeed, we set it to it
        if(newFallspeed <= fallSpeed)
          newFallspeed = fallSpeed;

        velocity += newFallspeed;
        //Then we do the falling
        transform.position += 
        //transform.Translate(
         // Vector3.ClampMagnitude(
          (down * velocity) * Time.deltaTime
         // , distanceToGround)
         // )
         ;
      }

      if(disableRotation == false)
      {
        //We align the worm
        transform.up = Vector3.Lerp(transform.up, -down, velocity * .10f);
      }
    }
    else
    {
      //Debug.LogError("Raycast did not found a walkable surface under me. (Maybe surface has the wrong layer?)");
    }
  }
}
