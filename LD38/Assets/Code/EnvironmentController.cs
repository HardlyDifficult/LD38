using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject PlanetObject;

    public static float ShrinkPerTurn = 0.01f; // In percentages.
    public static float ShrinkAnimationTime = 0.8f;
    public float ShrinkMinimumSize = 5.0f;

    private Vector3 _currentScale;

    public void Awake()
    {
        _currentScale = PlanetObject.transform.localScale;
        TurnController.onTurnChange += OnTurnChange;
    }

    public void FixedUpdate()
    {
        if (PlanetObject != null)
        {
            Vector3 scale = Vector3.Lerp(PlanetObject.transform.localScale, _currentScale,
                Time.fixedDeltaTime * ShrinkAnimationTime);

            PlanetObject.transform.localScale = scale;
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

