using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

  [Header("Movement")]
  public float moveSpeed = 10f; //How fast we move
  public float jumpStrengh = 1f; //How high we jump
  Gravity gravity;

 
  protected void Start()
  {
    gravity = GetComponent<Gravity>();
  }

  private void Update()
  {
    MoveWorm();
    Jump();
  }

  /// <summary>
  /// Gets the input horizontal and moves the worm forward or backward
  /// </summary>
  public void MoveWorm()
  {
    transform.Translate((Vector3.forward * Input.GetAxis("Horizontal")) * Time.deltaTime * moveSpeed);
  }

  /// <summary>
  /// Lets the worm jump
  /// </summary>
  public void Jump()
  {
    if(Input.GetAxis("Jump") > 0)
    {
      if(gravity.isGrounded)
      {
        transform.Translate(Vector3.up * jumpStrengh);
        gravity.isGrounded = false;
      }
    }
  }

}
