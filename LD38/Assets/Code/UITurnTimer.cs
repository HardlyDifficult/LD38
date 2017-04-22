using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnTimer : MonoBehaviour
{
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }

  public void Update()
  {
    text.text = Mathf.RoundToInt(TurnController.timeRemaining * Time.fixedDeltaTime).ToString();
  }
}
