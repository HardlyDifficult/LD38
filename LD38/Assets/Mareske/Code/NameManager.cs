using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

    [Header("Game Design")]
    public List<string> nameList = new List<string>();
    private List<string> copiedList = new List<string>();

    public static NameManager me;

    private void Awake()
    {
        me = this;
        copiedList = nameList;
    }

    private void Start()
    {
        SetNames();
    }

    /// <summary>
    /// Gives the worms random names
    /// </summary>
    public void SetNames()
    {
        //We get all the name display
        GameObject[] tempDis = GameObject.FindGameObjectsWithTag("NameDisplay");

        //Then we go trough each
        foreach (GameObject go in tempDis)
        {
            int rnd = Random.Range(0, copiedList.Count); //We get a random name
            go.GetComponent<NameDisplayer>().SetName(copiedList[rnd]); //Then we apply the name
            copiedList.RemoveAt(rnd); //Then we delete the name, so that we dont have the same name twice
        }
    }
}
