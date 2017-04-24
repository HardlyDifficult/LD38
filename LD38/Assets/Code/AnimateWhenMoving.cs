using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWhenMoving : MonoBehaviour {
  Animator animator;
  Vector3 previousPosition;
  public GameObject dust;

	// Use this for initialization
	void Start () {
    animator = GetComponent<Animator>();
    previousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
    var deltaPosition = previousPosition - transform.position;
    if(deltaPosition.sqrMagnitude < .0001f)
    {  
      animator.enabled = false;
    } else
    {
      animator.enabled = true;
    }

    animator.speed = Mathf.Min(2, deltaPosition.sqrMagnitude * 1000);

    var targetScale = Vector3.one * Mathf.Min(1, deltaPosition.sqrMagnitude * 1000);
    dust.transform.localScale = Vector3.Lerp(dust.transform.localScale, targetScale, .01f);

    previousPosition = transform.position;
	}
}
