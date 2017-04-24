using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameDisplayer : MonoBehaviour
{
  public Text myTextMesh;

  /// <summary>
  /// Sets the display name
  /// </summary>
  /// <param name="_name"></param>
  public void SetName(string _name)
  {
    GetComponent<PhotonView>().RPC("DoSetName", PhotonTargets.AllBuffered, _name);
  }

  [PunRPC]
  void DoSetName(string _name)
  {
    myTextMesh.text = _name;
  }
}
