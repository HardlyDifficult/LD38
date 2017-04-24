using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOutOfBounds : MonoBehaviour {
  
	// Update is called once per frame
	void Update () {
    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }

		if(transform.position.sqrMagnitude > 1000000)
    {
      PhotonNetwork.Destroy(gameObject);
    }
	}
}
