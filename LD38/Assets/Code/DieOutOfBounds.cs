using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOutOfBounds : MonoBehaviour {
  
	// Update is called once per frame
	void Update () {
		if(transform.position.sqrMagnitude > 10000)
    {
      Destroy(gameObject);
    }
	}
}
