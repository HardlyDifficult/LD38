using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorByTeam : MonoBehaviour {
  Material mat;

	// Use this for initialization
	void Start () {
    var playerInfo = GetComponentInParent<PlayerInfo>();
    mat = GetComponent<MeshRenderer>().material;
    switch(playerInfo.team.Id)
    {
      case 0:
        mat.color = Color.red;
        break;
      default:
      case 1:
        mat.color = Color.blue;
        break;
    }
  }

  private void OnDestroy()
  {
    Destroy(mat);
  }
}
