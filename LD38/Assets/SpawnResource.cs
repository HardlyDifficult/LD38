using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResource : MonoBehaviour {

  public string resource;

	// Use this for initialization
	void Start () {
    var res = Resources.Load<GameObject>(resource);
    var go = Instantiate(res, transform.position, transform.rotation);
    go.transform.localScale = transform.localScale;
	}
}
