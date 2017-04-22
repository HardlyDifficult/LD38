using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
  TeamPlayer teamPlayer;

  [Header("Movement")]
  public float moveSpeed = 10f; //How fast we move
  public float jumpStrengh = 1f; //How high we jump
  Gravity gravity;

  protected void Start()
  {
    teamPlayer = GetComponent<TeamPlayer>();
    gravity = GetComponent<Gravity>();
  }

  private void Update()
  {
    if(teamPlayer.isMyTurn == false)
    {
      return;
    }

    MoveWorm();
    Jump();
  }

  /// <summary>
  /// Gets the input horizontal and moves the worm forward or backward
  /// </summary>
  public void MoveWorm()
  {
    transform.Translate((Vector2.right * Input.GetAxis("Horizontal")) * Time.deltaTime * moveSpeed);
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
        transform.Translate(Vector2.up * jumpStrengh);
        gravity.isGrounded = false;
      }
    }
  }

}
