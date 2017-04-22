using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
  public GameObject[] treeList;

  public void Awake()
  {
    for(int i = 0; i < treeList.Length; i++)
    {
      for(int j = 0; j < UnityEngine.Random.Range(1, 10); j++)
      {
        Spawn(treeList[i]);
      }
    }
  }

  void Spawn(
    GameObject gameObject)
  {
    Vector3 randomPosition;
    do
    {
      randomPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f),
        UnityEngine.Random.Range(-1f, 1f),
        UnityEngine.Random.Range(-1f, 1f)).normalized;
    } while(randomPosition.sqrMagnitude < .1f);

    randomPosition *= 100;

    Vector3 toCenter = Vector3.zero - randomPosition;
    RaycastHit hit;
    float maxDistance = 0;
    if(Physics.Raycast(randomPosition, toCenter, out hit, Mathf.Infinity,
      LayerMask.GetMask(new[] { "Planet" })))
    {
      maxDistance = hit.distance;
      Quaternion rotation = Quaternion.LookRotation(-randomPosition) * Quaternion.Euler(-90, 0, 0);

      if(Test(randomPosition, rotation, toCenter, hit, ref maxDistance, Vector3.right) == false)
      {
        return;
      }
      if(Test(randomPosition, rotation, toCenter, hit, ref maxDistance, Vector3.left) == false)
      {
        return;
      }
      if(Test(randomPosition, rotation, toCenter, hit, ref maxDistance, Vector3.forward) == false)
      {
        return;
      }
      if(Test(randomPosition, rotation, toCenter, hit, ref maxDistance, Vector3.back) == false)
      {
        return;
      }

      Vector3 hitPoint = hit.point + rotation * Vector3.down * .2f;
      GameObject newTree = Instantiate(gameObject, hitPoint, rotation);
      float width = UnityEngine.Random.Range(.5f, 1.5f);
      newTree.transform.localScale = new Vector3(width, UnityEngine.Random.Range(.5f, 1.5f), width);
    }
  }

  bool Test(Vector3 randomPosition, Quaternion rotation, Vector3 toCenter, RaycastHit hit, ref float maxDistance, Vector3 delta)
  {
    RaycastHit hit2;
    if(Physics.Raycast(randomPosition + rotation * delta * .2f, toCenter, out hit2, Mathf.Infinity,
    LayerMask.GetMask(new[] { "Planet" })))
    {
      if(Math.Abs(hit2.distance - hit.distance) > .1f)
      {
        return false;
      }
      maxDistance = Math.Max(maxDistance, hit2.distance);
    }
    else
    {
      return false;
    }

    return true;
  }
}
