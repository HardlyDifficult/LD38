using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOutOfBounds : MonoBehaviour {
  
	// Update is called once per frame
	void Update () {
    if(PhotonView.Get(this).isMine == false)
    {
      return;
    }

		if(transform.position.sqrMagnitude > 100000)
    {
      PhotonNetwork.Destroy(gameObject);
    }
	}
}
