using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add jump
/// Smooth the rotation due to rough normals
/// Should face the direction we are headed
/// </summary>

public class WormMovement : MonoBehaviour
{
  TeamPlayer teamPlayer;

  [Header("Movement")]
  public float moveSpeed = 10f; //How fast we move
  public float jumpStrengh = 1f; //How high we jump
  Gravity2 gravity;
  Rigidbody body;

  protected void Start()
  {
    body = GetComponent<Rigidbody>();
    teamPlayer = GetComponent<TeamPlayer>();
    gravity = GetComponent<Gravity2>();
  }

  private void Update()
  {
    if(teamPlayer.isMyTurn == false)
    {
      return;
    }

    MoveWorm();
    //Jump();
  }

  /// <summary>
  /// Gets the input horizontal and moves the worm forward or backward
  /// </summary>
  public void MoveWorm()
  {
    body.AddForce((transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * moveSpeed
      +
      (transform.forward * Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);
    //transform.Translate((Vector3.right * Input.GetAxis("Horizontal")) * Time.deltaTime * moveSpeed);
    //transform.Translate((Vector3.forward * Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);


  }


  private void FixedUpdate()
  {

    if(gravity.isGrounded && Input.GetAxis("Jump") > 0)
    {
      body.AddForce(transform.up * jumpStrengh);
    }
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
