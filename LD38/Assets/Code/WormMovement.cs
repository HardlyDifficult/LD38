using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

  [Header("Raycast")]
  public bool isGrounded = false;
  public float rayLength;

  [Header("Movement")]
  public float moveSpeed = 1f;
  public float jumpStrengh = 1f;
  public float fallSpeed = 1f;

  private void Update()
  {
    MoveWorm();
    CheckGravity();
  }

  /// <summary>
  /// Gets the input horizontal and moves the worm forward or backward
  /// </summary>
  public void MoveWorm()
  {
    this.transform.Translate((Vector3.forward * Input.GetAxis("Horizontal")) * Time.deltaTime);
  }

  /// <summary>
  /// Checks if we are in the air and need to fall
  /// </summary>
  private void CheckGravity()
  {
    RaycastHit hit;
    Vector3 down = Vector3.zero - transform.position;

    if(Physics.Raycast(transform.position, down, out hit, Mathf.Infinity))
    {
      float distanceToGround = Vector3.Distance(hit.point, this.transform.position);
      //Check the distance to the ground
      if(distanceToGround <= 0.1f)
      {

        isGrounded = true;
      }

      //If the worm is not grounded then we pull him down
      if(!isGrounded)
      {
        this.transform.Translate(Vector3.ClampMagnitude((Vector3.down * fallSpeed) * Time.deltaTime, distanceToGround * 10000));
      }

      //We align the worm
      this.transform.up = Vector3.Lerp(this.transform.up, hit.normal, 10);
    }
    else
    {
      Debug.LogError("Why is there no fucking planet under me?");
    }


  }
}
