using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIn : MonoBehaviour {
  public float timeRemaining = 5;
  public float explosionIntensity = 10;
  bool isDead;

  private void OnDestroy()
  {
    isDead = true;
  }

  void Update () {
		if(isDead || PhotonView.Get(this).isMine == false)
    {
      return;
    }

    timeRemaining -= Time.deltaTime;
    if(timeRemaining <= 0)
    {
      var newExplosion = PhotonNetwork.Instantiate("Explosion3", transform.position, Quaternion.identity, 0);
      newExplosion.transform.localScale = Vector3.one * explosionIntensity;
      PhotonNetwork.Destroy(gameObject);
      isDead = true;
    }
	}
}
