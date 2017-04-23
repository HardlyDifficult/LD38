using System;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
  public GameObject[] treeList;

  private bool _treesDirty = false;
  private List<GameObject> _spawnedTreeList = new List<GameObject>();

  public void Start()
  {
    TurnController.onTurnChange += OnTurnChange;

    for(int i = 0; i < treeList.Length; i++)
    {
      for(int j = 0; j < UnityEngine.Random.Range(1, 10); j++)
      {
        Spawn(treeList[i]);
      }
    }
  }

  private void OnTurnChange()
  {
    _treesDirty = true;
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
      float width = UnityEngine.Random.Range(.2f, 1.0f);
      newTree.transform.localScale = new Vector3(width, UnityEngine.Random.Range(.5f, 1.5f), width);
      newTree.transform.localPosition = hitPoint;

      _spawnedTreeList.Add(newTree);
    }
  }

  void LateUpdate()
  {
    if(_treesDirty)
    {
      float avgDistance = 0.0f;
      for(int i = 0; i < _spawnedTreeList.Count; i++)
      {
        Transform tree = _spawnedTreeList[i].transform;

        Vector3 treeScale = Vector3.Lerp(
            tree.localScale,
            tree.localScale * (1.0f - EnvironmentController.ShrinkPerTurn),
            Time.deltaTime * EnvironmentController.ShrinkAnimationTime
        );

        tree.localScale = treeScale;

        Vector3 position = RecalculatePosition(tree);

        avgDistance += (position - tree.position).magnitude;

        tree.position = Vector3.Lerp(
            tree.position,
            position, Time.deltaTime *
            EnvironmentController.ShrinkAnimationTime
        );
      }

      avgDistance /= _spawnedTreeList.Count;

      if(avgDistance < 0.05f)
        _treesDirty = false;
    }

  }

  Vector3 RecalculatePosition(Transform tree)
  {
    RaycastHit hit;
    Vector3 toCenter = Vector3.zero - tree.position;

    if(Physics.Raycast(tree.position + tree.up, toCenter, out hit, Mathf.Infinity,
        LayerMask.GetMask(new[] { "Planet" })))
    {
      Vector3 hitPoint = hit.point - tree.up + tree.rotation * Vector3.down * .2f;
      return hitPoint;
    }

    return tree.position;
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
