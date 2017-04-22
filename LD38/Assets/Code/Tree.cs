using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

  protected void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.GetComponent<Tree>() != null)
    {
      Destroy(gameObject);
    }
  }
}
