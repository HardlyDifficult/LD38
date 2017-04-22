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
    RaycastHit2D hit;

    //The raycast starts under us
    Vector2 down = Vector2.zero - (Vector2)transform.position;  //transform.TransformDirection(Vector3.down);

    //We start the raycast
    hit = Physics2D.Raycast(transform.position, down, Mathf.Infinity, rayLayer);
    if(hit.transform != null)
    {
      //We store the distance to the ground for later usage
      distanceToGround = Vector3.Distance(hit.point, transform.position);

      //if we are near the surface then we are grounded and dont fall anymore
      if(distanceToGround <= 0.01f)
      {
        isGrounded = true;
        velocity = 0;
        if(onGrounded != null)
        {
          onGrounded.Invoke();
        }
      }
      else
      {
        isGrounded = false;
      }

      //If the worm is not grounded then we pull him down to fall
      if(!isGrounded)
      {
        //Calculating new fallspeed, to fall faster the further we are from the surface
        float newFallspeed = fallSpeed * distanceToGround;
        //If the fallspeed is slower then the main fallspeed, we set it to it
        if(newFallspeed <= fallSpeed)
          newFallspeed = fallSpeed;

        velocity += newFallspeed;
        //Then we do the falling
        transform.position += (Vector3)
          //transform.Translate(
          // Vector3.ClampMagnitude(
          (down * velocity) * Time.deltaTime
         // , distanceToGround)
         // )
         ;
      }

    }
    else
    {
      //Debug.LogError("Raycast did not found a walkable surface under me. (Maybe surface has the wrong layer?)");
    }

    if(disableRotation == false)
    {
      //We align the worm
      //old way transform.up = Vector2.Lerp(transform.up, -down, velocity * .10f);

      transform.rotation = Quaternion.Lerp(transform.rotation,
        Quaternion.LookRotation(transform.forward, -down),
       velocity * 10f);
    }
  }
}
