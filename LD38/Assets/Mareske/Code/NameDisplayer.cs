using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameDisplayer : MonoBehaviour {

    [Header("Referenzes")]
    private Text myTextMesh;

    private void Awake()
    {
        myTextMesh = GetComponent<Text>();
    }
  
    /// <summary>
    /// Sets the display name
    /// </summary>
    /// <param name="_name"></param>
    public void SetName(string _name)
    {
        myTextMesh.text = _name;
    }
}
