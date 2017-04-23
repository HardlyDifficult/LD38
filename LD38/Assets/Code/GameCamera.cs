using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 10;
    const float Zoom = 10;

    protected void Update()
    {
        if (TurnController.CurrentPlayer == null)
            return;

        Vector3 offset = TurnController.CurrentPlayer.transform.up * Zoom + -TurnController.CurrentPlayer.transform.forward * Zoom;
        Vector3 position = TurnController.CurrentPlayer.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(TurnController.CurrentPlayer.transform.position - transform.position,
          TurnController.CurrentPlayer.transform.up);
    }
}
