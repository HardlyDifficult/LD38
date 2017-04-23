using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
  public float moveSpeed = 10f; //How fast we move
  public float jumpStrengh = 1f; //How high we jump

  TeamPlayer teamPlayer;
  Gravity gravity;
  Rigidbody body;

  protected void Start()
  {
    body = GetComponent<Rigidbody>();
    teamPlayer = GetComponent<TeamPlayer>();
    gravity = GetComponent<Gravity>();
  }

  protected void FixedUpdate()
  {
    if(teamPlayer.isMyTurn == false)
    {
      return;
    }

    MoveWorm();
    Jump();
  }

  void MoveWorm()
  {
    if(gravity.isGrounded)
    { 
      body.AddForce((transform.right * Input.GetAxis("Vertical")
        //+ transform.forward * Input.GetAxis("Vertical") Switching to turn instead of move that direction
        )
        * Time.deltaTime * moveSpeed);
    }

    gravity.turnOffset = Quaternion.Euler(0, Input.GetAxis("Horizontal"), 0);
  }

  void Jump()
  {
    if(gravity.isGrounded && Input.GetAxis("Jump") > 0)
    {
      body.AddForce(transform.up * jumpStrengh);
    }
  }
}
