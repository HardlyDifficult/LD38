using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
  public GameObject PlanetObject;

  public static float ShrinkPerTurn = 0.1f; // In percentages.
  public static float ShrinkAnimationTime = 0.8f;
  public float ShrinkMinimumSize = 5.0f;

  private Vector3 _currentScale;

  public void Start()
  {
    _currentScale = PlanetObject.transform.localScale;
    TurnController.onTurnChange += OnTurnChange;
  }

  public void Update()
  {
    if(PlanetObject != null)
    {
      PlanetObject.transform.localScale = Vector3.Lerp(PlanetObject.transform.localScale, _currentScale,
          Time.deltaTime * ShrinkAnimationTime); 
    }
  }

  private void OnTurnChange()
  {
    _currentScale *= 1.0f - ShrinkPerTurn;

    _currentScale = new Vector3(
        Mathf.Max(_currentScale.x, ShrinkMinimumSize),
        Mathf.Max(_currentScale.y, ShrinkMinimumSize),
        Mathf.Max(_currentScale.z, ShrinkMinimumSize)
    );
  }
}

